
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

var canvas = document.getElementById("canvas");
var ctx = canvas.getContext('2d');
ctx.canvas.width = window.innerWidth;
ctx.canvas.height = window.innerHeight;
var s = 21;
var guid = 0;
connection.on("ReceiveData", (tmp) => {
    if (tmp.gameStat) {
        document.getElementById("k").style.visibility = "hidden";
    }
    else {
        document.getElementById("k").style.visibility = "visible";
    }
    guid = tmp.gameId;
    var map = tmp.map;
    ctx.fillStyle = 'black';
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    var node = document.getElementById("score");
    node.innerHTML = tmp.score;
    var node = document.getElementById("lives");
    node.innerHTML = tmp.lives;
    if (tmp.gameStat == 3) {
        window.location.href = "\RecordScore?id=" + guid;
        }
    for (var i = 0; i < map[0].length; i++) {
        for (var j = 0; j < map.length; j++) {
            if (map[j][i] == '█') {
                ctx.fillStyle = "blue";
                ctx.fillRect(i * s, j * s, s, s);
            }
            if (map[j][i] == 'CherryFood') {
                var img = new Image();
                img.src = "/images/cherry.png";
                ctx.drawImage(img, i * s, j * s, s, s);
            }
            
            if (map[j][i] == 'f') {
                var img = new Image();
                img.src = "/images/frightened.png";
                ctx.drawImage(img, i * s, j * s, s, s);
            } if (map[j][i] == 'd') {
                var img = new Image();
                img.src = "/images/deadghost.png";
                ctx.drawImage(img, i * s, j * s, s, s);
            }
            if (map[j][i] == '.') {
                ctx.fillStyle = "white";
                ctx.beginPath();
                ctx.arc(i * s + s / 2, j * s + s / 2, 2, 0, 2 * Math.PI);
                ctx.closePath();
                ctx.fill();
            }
            if (map[j][i] == '#') {
                ctx.fillStyle = "white";
                ctx.beginPath();
                ctx.arc(i * s + s / 2, j * s + s / 2, 7, 0, 2 * Math.PI);
                ctx.closePath();
                ctx.fill();
            }
        }
    }
    var img = new Image();
    img.src = "/images/pacman.png";
    switch (tmp.direction) {
        case 2:
            img.src = "/images/pacmanleft.png";
            break;
        case -2:
            img.src = "/images/pacmanright.png";
            break;
        case 1:
            img.src = "/images/pacmanup.png";
            break;
        case -1:
            img.src = "/images/pacmandown.png";
            break;
    }
    var px = tmp.movebleObjects[0].xp;
    var py = tmp.movebleObjects[0].yp;
    ctx.drawImage(img, (s * py), (s * px), s, s);

    var gx = 0;
    var gy = 0;
    var name;
    for (var i = 1; i < tmp.movebleObjects.length; i++) {
        gx = tmp.movebleObjects[i].xp;
        gy = tmp.movebleObjects[i].yp;
        console.log(tmp);
        name = tmp.movebleObjects[i].name;
        var img = new Image();
        img.src = "/images/" + name + ".png";
        ctx.drawImage(img, s * gy, s * gx, s, s);
    }
});

document.addEventListener("DOMContentLoaded", function () {
    setInterval(function () {
        refresh();
    }, 50);
});
document.addEventListener('keydown', (event) => {
    const keyName = event.key;
    if (event.key == "ArrowRight") {
        connection.invoke("MoveRight", guid).catch(err => console.error(err.toString()));
    }
    if (event.key == "ArrowLeft") {
        connection.invoke("MoveLeft", guid).catch(err => console.error(err.toString()));
    }
    if (event.key == "ArrowUp") {
        connection.invoke("MoveUp", guid).catch(err => console.error(err.toString()));
    }
    if (event.key == "ArrowDown") {
        connection.invoke("MoveDown", guid).catch(err => console.error(err.toString()));
    }
});
document.getElementById("restartButtom").addEventListener("click", event => {
    connection.invoke("Restar", guid).catch(err => console.error(err.toString()));
    event.preventDefault();
});

function refresh() {
    connection.invoke("RequestData", guid).catch(err => console.error(err.toString()));
}


connection.start().catch(err => console.error(err.toString()));

