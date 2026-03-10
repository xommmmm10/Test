updateUI();

function updateUI(){

document.getElementById("money").innerText = gameState.money;
document.getElementById("fans").innerText = gameState.fans;
document.getElementById("studio").innerText = gameState.studioLevel;

}

function developGame(){

let name = document.getElementById("gameName").value;
let genre = document.getElementById("genre").value;
let platform = document.getElementById("platform").value;

let cost = 2000 + (gameState.employees*500);

if(gameState.money < cost){
alert("Not enough money");
return;
}

gameState.money -= cost;

let quality =
Math.floor(Math.random()*10)
+ gameState.techLevel
+ gameState.studioLevel
+ gameState.employees;

let sales = quality * 1000;

gameState.money += sales;
gameState.fans += quality * 50;

addHistory(name,genre,platform,quality);

updateUI();

}

function addHistory(name,genre,platform,score){

let li = document.createElement("li");

li.innerText =
name+" ("+genre+" / "+platform+") Score: "+score;

document.getElementById("history").appendChild(li);

}

function upgradeStudio(){

if(gameState.money < 20000) return;

gameState.money -= 20000;
gameState.studioLevel++;

updateUI();

}

function hireEmployee(){

if(gameState.money < 5000) return;

gameState.money -= 5000;
gameState.employees++;

updateUI();

}

function researchTech(){

if(gameState.money < 8000) return;

gameState.money -= 8000;
gameState.techLevel++;

updateUI();

}
