const net = require('net');
const os = require('os');
const version = "0.1";

function startServer() {
	var connectionId;
	var isLE = os.endianness() === 'LE';
	var host = process.argv.length > 2 && process.argv.pop();
	var port = 4435;
	var socket = net.createConnection(host || { port }, onConnect);
	
	socket.on('data', function onData(data) {
		var view = new DataView(data.buffer);
		var actionNumber = view.getInt32(4, isLE);
		var action = convertServerCode(actionNumber);
		
		socket.emit(action, view, socket);
	});
	socket.on('end', onDisconnect);
	socket.on('Authentication', onAuth);
	socket.on('UpdateEntity', onUpdateEntity)
}

function onAuth(view, socket) {
	console.log(`action -> authentication`);
	connectionId = view.getInt32(8, isLE);
	
	var actionBuff = Int32Array.from([convertAction('Authentication')]);
	var versionBuff = Buffer.from(version, "ascii");
	var packageLen = actionBuff.byteLength + versionBuff.byteLength;
	var final = new Uint8Array(4 + packageLen);

	final.set(Int32Array.from([packageLen]), 0);
	final.set(actionBuff, 4);
	final.set(versionBuff, 8);
	socket.write(final);
}

function onUpdateEntity(view) {
	console.log('action -> update entity');
	
	var enc = new TextDecoder("ascii", { fatal: true });
	var strBuffer = new DataView(view.buffer, 12);
	var guid = enc.decode(strBuffer, { stream: true });
	guid = guid.substr(0, guid.indexOf('\x00'));
	var bytePos = 8 + guid.length;

	var posX = view.getFloat32(bytePos + 4, isLE);
	var posY = view.getFloat32(bytePos + 8, isLE);
	var posZ = view.getFloat32(bytePos + 12, isLE);
	var rotX = view.getFloat32(bytePos + 16, isLE);
	var rotY = view.getFloat32(bytePos + 20, isLE);
	var rotZ = view.getFloat32(bytePos + 24, isLE);
	var rotW = view.getFloat32(bytePos + 28, isLE);
	
	console.log("guid", guid);
	console.log(`pos (x,y,z) -> (${posX}, ${posY}, ${posZ})`);
	console.log(`rot (x,y,z,w) -> (${rotX}, ${rotY}, ${rotZ}, ${rotW})`);
}

function onServerData(view) {
	console.log('action -> request server data');
	console.log(view);
}

function onConnect() {
	console.log(`connected to server at ${host || port}`);
}

function onDisconnect() {
	console.log('disconnected from server');
}

function showUsage() {
	console.log("Usage: master-server.js <hosts>");
	console.log("Args:");
	console.log("	hosts - the a comma separated list of hosts (e.g. 'domain1:1234,domain2:12775')")
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
if (process.argv.includes('--help')) {
	showUsage();
}
startServer();