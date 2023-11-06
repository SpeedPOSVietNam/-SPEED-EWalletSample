var webSocket   = null;
var ws_protocol = "ws";
var ws_hostname = "127.0.0.1";
var ws_port     = 9000;
var ws_endpoint = "/websession";
var ws_reconnect = false;
var contentLogo = null;

function onConnect() {
	
	if(webSocket != null) {
		if (webSocket.readyState != WebSocket.OPEN) {
			openWSConnection(ws_protocol, ws_hostname, ws_port, ws_endpoint);
		}
	}
	else
	{
		openWSConnection(ws_protocol, ws_hostname, ws_port, ws_endpoint);
	}
	
	
}

function onDisconnect() {
    webSocket.close();
}

function openWSConnection(protocol, hostname, port, endpoint) {
	if(!ws_reconnect)
	{
		ws_reconnect = true;
		var webSocketURL = null;
		webSocketURL = protocol + "://" + hostname + ":" + port + endpoint;
		try {
			webSocket = new WebSocket(webSocketURL);
			webSocket.onopen = function(openEvent) {
				//console.log("WebSocket OPEN: " + JSON.stringify(openEvent, null, 4));	
			};
			webSocket.onclose = function (closeEvent) {
				//console.log("WebSocket CLOSE: " + JSON.stringify(closeEvent, null, 4));
				ws_reconnect = false;
				$("#divQRCode").html(contentLogo);
			};
			webSocket.onerror = function (errorEvent) {
				//console.log("WebSocket ERROR: " + JSON.stringify(errorEvent, null, 4));
				ws_reconnect = false;
			};
			webSocket.onmessage = function (messageEvent) {
				var QRText = messageEvent.data;
				if(QRText.indexOf("data:image") == -1)
				{
					$("#divQRCode").html("");
					
					$("#divQRCode").qrcode({
						text	: QRText,
						width   : 140,
						height  : 140
					});
				}
				else
				{
					$("#imgQRCode").attr("src", QRText);
				}
			};
		} catch (exception) {
			//console.log(exception.toString());
		}
	}
}

function onSend(msg) {
	try {
		if (webSocket.readyState != WebSocket.OPEN) {
			console.log("webSocket is not open: " + webSocket.readyState);
			return;
		}		
		webSocket.send(msg);
	} catch (exception) {
        console.log(exception.toString());
    }
}

$(document).ready(function() {
	contentLogo = $("#divQRCode").html();
	onConnect();
    ws_reconnect = setInterval(onConnect,1000);
});