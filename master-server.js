const net = require('net');
const os = require('os');
const version = "0.1";
const isLE = os.endianness() === 'LE';
const isVerbose = process.argv.includes('--verbose');

function startServer() {
	var port = Number.parseInt(getCliArg('port', 4000));
	var hosts = getCliArg('hosts', '');
	var masterServer = net.createServer();
	var servers = hosts.split(',').map(connectServer);
	
	masterServer.listen(port, () => console.log(`master server is listening to port ${port}`));
	masterServer.on('connection', function onConnect(socket) {
		logSocket(socket, `Sending client servers list`);
		socket.write(jsonServerList(servers) + '\n');
		socket.on('data', () => {
			socket.write(jsonServerList(servers) + '\n');
		})
		socket.pipe(socket);
	});
	masterServer.on('error', function onError(err) {
		servers.forEach(server => server.end());
		console.log(`Master server on port ${port} failed to ${err.message}`);
	});
	console.log(`connecting to ${servers.length || 1} server${hosts.length > 1 && 's' || ''}...`);
}

function getCliArg(arg, def) {
	var idx = process.argv.indexOf(`--${arg}`);
	var arg = process.argv[idx + 1];
	if (idx === -1 || !arg || arg.startsWith('-')) {
		return def;
	}
	return arg;
}

function connectServer(host) {
	let lastPing = new Date();
	let socket;
	
	if (typeof host === 'string' && host.length > 0) {
		host = host.trim();
	} else {
		host = { port: 4435 };
	}

	socket = net.createConnection(host, () => onConnect(socket));

	socket.on('error', function onError(err) {
		console.log('Failed to ' + err.message);
	})
	socket.on('data', function onData(data) {
		var view = new DataView(data.buffer);
		var actionNumber = view.getInt32(4, isLE);
		var action = convertServerCode(actionNumber);
		socket.ping = Date.now() - lastPing.getTime();
		socket.emit(action, view, socket);
	});
	socket.on('end', () => onDisconnect(socket));
	socket.on('Authentication', onAuth);
	socket.on('UpdateEntity', onUpdateEntity)
	return socket;
}

function onAuth(view, socket) {
	let actionBuff = Int32Array.from([convertAction('Authentication')]);
	let versionBuff = Buffer.from(version, "ascii");
	let packageLen = actionBuff.byteLength + versionBuff.byteLength;
	let final = new Uint8Array(4 + packageLen);

	final.set(Int32Array.from([packageLen]), 0);
	final.set(actionBuff, 4);
	final.set(versionBuff, 8);
	socket.write(final);

	if (isVerbose) {
		logSocket(socket, `action -> authentication`);
	}
}

function onUpdateEntity(view, socket) {	
	let enc = new TextDecoder("ascii", { fatal: true });
	let strBuffer = new DataView(view.buffer, 12);
	let guid = enc.decode(strBuffer, { stream: true });
	guid = guid.substr(0, guid.indexOf('\x00'));
	let bytePos = 8 + guid.length;

	let posX = view.getFloat32(bytePos + 4, isLE);
	let posY = view.getFloat32(bytePos + 8, isLE);
	let posZ = view.getFloat32(bytePos + 12, isLE);
	let rotX = view.getFloat32(bytePos + 16, isLE);
	let rotY = view.getFloat32(bytePos + 20, isLE);
	let rotZ = view.getFloat32(bytePos + 24, isLE);
	let rotW = view.getFloat32(bytePos + 28, isLE);
	
	if (isVerbose) {
		logSocket(socket, 'action -> update entity');
		logSocket(socket, "guid", guid);
		logSocket(socket, `pos (x,y,z) -> (${posX}, ${posY}, ${posZ})`);
		logSocket(socket, `rot (x,y,z,w) -> (${rotX}, ${rotY}, ${rotZ}, ${rotW})`);
	}
}

function jsonServerList(servers) {
	return JSON.stringify(servers.map(function mapServer(server) {
		return {
			host: server.remoteAddress,
			port: server.remotePort,
		};
	}))
}

function onConnect(socket) {
	logSocket(socket, `connected to server`);
}

function onDisconnect(socket) {
	logSocket(socket, 'disconnected from server');
}

function showUsage() {
	console.log("Usage: master-server.js <hosts>");
	console.log("Args:");
	console.log("	hosts - the a comma separated list of hosts (e.g. 'domain1:1234,domain2:12775')")
}

function logSocket(socket, ...msg) {
	process.stdout.write(`[${socket.remoteAddress}:${socket.remotePort}] `);
	console.log(msg.join(' '))
}

function convertAction(action) {
	if (action === 'InstantiateRemotePlayer') return 1; 
    if (action === 'InstantiateLocalPlayer') return 2; 
    if (action === 'DestoryPlayer') return 3; 
    if (action === 'Authentication') return 8; 
    if (action === 'UpdatePlayer') return 5; 
    if (action === 'InstantiateWorldObjects') return 6; 
    if (action === 'SetNetworkName') return 7; 
    if (action === 'RequestServerData') return 8; 
    if (action === 'UpdatePlayerAnimator') return 9; 
    if (action === 'SendAuthenticationConfirmed') return 10; 
    if (action === 'UpdateEntity') return 11; 
}

function convertServerCode(number) {
	if (number === 1) return 'InstantiateRemotePlayer'
    if (number === 2) return 'InstantiateLocalPlayer'
    if (number === 3) return 'DestoryPlayer'
    if (number === 4) return 'Authentication'
    if (number === 5) return 'UpdatePlayer'
    if (number === 6) return 'InstantiateWorldObjects'
    if (number === 7) return 'SetNetworkName'
    if (number === 8) return 'RequestServerData'
    if (number === 9) return 'UpdatePlayerAnimator'
    if (number === 10) return 'SendAuthenticationConfirmed'
    if (number === 11) return 'UpdateEntity'
}

function start() {
	if (process.argv.includes('--help')) {
		showUsage();
	} else {
		startServer();
	}
}
start();