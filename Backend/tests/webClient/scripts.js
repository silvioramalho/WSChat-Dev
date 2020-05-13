/*********************************************
 * PayloadInterface:
 * {
 *      id: string;
 *      event: string;
 *      sendDate: Date;
 *      user: UserInterface;
 *      room: RoomInterface;
 *      messageText: string;
 *      TargetUserId: string;
 *      isPrivate: boolean;
 * }
 * 
 * UserInterface:
 * {
 *      idConnection: string;
 *      nickname: string;
 * }
 * 
 * RoomInterface:
 * {
 *      idRoom: int;
 *      roomName: string;
 * }
 * 
 *
 * Events:
 *      1   =   SocketConnect
 *      2   =   RegisterUser
 *      3   =   EnterRoom
 *      4   =   Messaging
 *      5   =   ExitRoom
 *      6   =   CreateRoom
 *      7   =   UpdateUserList
 *      8   =   UpdateRoomList
 *      9   =   WelcomeMessage
 *      10  =   GoodbyeMessage
 *      11  =   SocketDisconnect 
 *      500 =   Error
 *
 *
 ********************************************/

function getDate() {
  return new Date();
}

function formatDate(strDate){
  var date = new Date(strDate),
      day  = date.getDate().toString().padStart(2, '0'),
      month  = (date.getMonth()+1).toString().padStart(2, '0'), 
      year  = date.getFullYear();
      hour = date.getHours().toString().padStart(2, '0'),
      minutes = date.getMinutes().toString().padStart(2, '0');
  return day+"/"+month+"/"+year+" "+hour+":"+minutes;
}

var data = {
  event: 1,
  sendDate: getDate(),
  nickname: '',
};

var currentIdConnection = '';

/*********************************************
 * Socket Connection
 ********************************************/

//var uri = 'wss://wschat-backend.herokuapp.com/ws';
var uri = 'ws://localhost:5000/ws';
var socket;

var connectButton = document.getElementById('connectButton');
var disconnectButton = document.getElementById('disconnectButton');


function appendItem(list, message) {
  var item = document.createElement('li');
  item.appendChild(document.createTextNode(message));
  list.appendChild(item);
}

function isConnect() {
  return socket && socket.readyState === 1;
}

function updateSocketStatus(status) {
  document.getElementById('socketStatus').textContent = status;
}

function connect() {
  //console.log(socket);
  if (!isConnect()) {
    socket = null;
    socket = new WebSocket(uri);
    socket.readyState = 1;
  }

  socket.onopen = function (e) {
    console.log('Connection established');
  };
  socket.onclose = function (e) {
    console.log('Connection closed', formatDate(getDate()));
    updateSocketStatus('Off Line');
  };

  socket.onmessage = function (e) {
    receivedData = JSON.parse(e.data);
    //appendItem(list, e.data);

    if (receivedData.user != undefined && receivedData.user != null)
    {
      var message = "[ "+ formatDate(receivedData.sendDate) + "] "
        + receivedData.user.nickname + " > " + receivedData.messageText;

      appendItem(list, message);
      updateScroll();
    }

    

    if (receivedData.idConnection) {
      currentIdConnection = receivedData.idConnection;
    }

       

    console.log(receivedData);
  };
}

function disconnect() {
  if (socket) {
    socket.close();
    socket = null;
    console.log('--------- close socket ---------');
  }
}

function sendMessage(message) {
  console.log('---- sending message ----', message);
  socket.send(JSON.stringify(message));
}

connectButton.addEventListener('click', function () {
  connect();
  updateSocketStatus('On Line');
});

disconnectButton.addEventListener('click', function () {
  disconnect();
  updateSocketStatus('Off Line');
});

/*********************************************
 * User
 ********************************************/

var registerButton = document.getElementById('registerButton');

registerButton.addEventListener('click', function () {
  if (isConnect()) {
    console.log('--------- register user ---------');
    data = {
      event: 2,
      //sendDate: getDate(),
      messageText: document.getElementById('nickname').value,
    };
    sendMessage(data);
  }
});

/*********************************************
 * Room
 ********************************************/

var enterRoomButton = document.getElementById('enterRoomButton');
var exitRoomButton = document.getElementById('exitRoomButton');
var createRoomButton = document.getElementById('createRoomButton');

enterRoomButton.addEventListener('click', function () {
  console.log('--------- enter room ---------');
  data = {
    event: 3,
    sendDate: getDate(),
    messageText: "1"
  };
  sendMessage(data);
});

exitRoomButton.addEventListener('click', function () {
  console.log('--------- exit room ---------');
  data = {
    event: 5,
    sendDate: getDate(),
  };
  sendMessage(data);
});

createRoomButton.addEventListener('click', function () {
  console.log('--------- create room ---------');
  data = {
    event: 6,
    sendDate: getDate(),
    messageText: document.getElementById("roomName").value
  };
  sendMessage(data);
});

/*********************************************
 * Messages
 ********************************************/

var clearButton = document.getElementById('clearButton');
var sendButton = document.getElementById('sendButton');
var list = document.getElementById('messages');
var chkIsPrivate = document.getElementById('chkIsPrivate');

function updateScroll(){
  var element = document.getElementById("messages");
  element.scrollTop = element.scrollHeight;
}

function clearInput(element) {
  element.value = '';
}

function clearMessages() {
  document.getElementById('messages').innerHTML = '';
}

clearButton.addEventListener('click', function () {
  clearMessages();
});

sendButton.addEventListener('click', function () {
  var targetUserId = document.getElementById('TargetUserId');
  data = {
    event: 4,
    sendDate: getDate(),
    messageText: document.getElementById('messageToSend').value,
  };

  if (chkIsPrivate.checked) {
    data.isPrivate = true;
  }

  if (targetUserId.value !== '')
  {
    data.targetUserId = targetUserId.value;
  }
  
  sendMessage(data);
  //clearInput(document.getElementById('messageToSend'));
});
