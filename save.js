function saveGame(){

localStorage.setItem(
"gamedev_save",
JSON.stringify(gameState)
);

alert("Game Saved");

}

function loadGame(){

let data = localStorage.getItem("gamedev_save");

if(data){

gameState = JSON.parse(data);

updateUI();

alert("Game Loaded");

}

}
