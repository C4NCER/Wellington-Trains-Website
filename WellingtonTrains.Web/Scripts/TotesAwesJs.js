var linesList = document.getElementById("DropDownListLines");
var fromList = document.getElementById("DropDownListFrom");
var toList = document.getElementById("DropDownListTo");
var dayList = document.getElementById("DropDownListDay");

var stationLines = [
    { station: "WELL", line: "JVL", value: "Wellington", rank: "1" },
    { station: "CROF", line: "JVL", value: "Crofton Downs", rank: "2" },
    { station: "NGAI", line: "JVL", value: "Ngaio", rank: "3" },
    { station: "AWAR", line: "JVL", value: "Awarua Street", rank: "4" },
    { station: "SIML", line: "JVL", value: "Simla Crescent", rank: "5" },
    { station: "BOXH", line: "JVL", value: "Box Hill", rank: "6" },
    { station: "KHAN", line: "JVL", value: "Khandallah", rank: "7" },
    { station: "RARO", line: "JVL", value: "Raroa", rank: "8" },
    { station: "JOHN", line: "JVL", value: "Johnsonville", rank: "9" },
    { station: "WELL", line: "HVL", value: "Wellington", rank: "1" },
    { station: "NGAU", line: "HVL", value: "Ngauranga", rank: "3" },
    { station: "PETO", line: "HVL", value: "Petone", rank: "4" },
    { station: "AVA", line: "HVL", value: "Ava", rank: "5" },
    { station: "WOBU", line: "HVL", value: "Woburn", rank: "6" },
    { station: "WATE", line: "HVL", value: "Waterloo", rank: "7" },
    { station: "EPUN", line: "HVL", value: "Epuni", rank: "8" },
    { station: "NAEN", line: "HVL", value: "Naenae", rank: "9" },
    { station: "WING", line: "HVL", value: "Wingate", rank: "10" },
    { station: "TAIT", line: "HVL", value: "Taita", rank: "11" },
    { station: "POMA", line: "HVL", value: "Pomare", rank: "12" },
    { station: "MANO", line: "HVL", value: "Manor Park", rank: "13" },
    { station: "SILV", line: "HVL", value: "Silverstream", rank: "14" },
    { station: "HERE", line: "HVL", value: "Heretaunga", rank: "15" },
    { station: "TREN", line: "HVL", value: "Trentham", rank: "16" },
    { station: "WALL", line: "HVL", value: "Wallaceville", rank: "17" },
    { station: "UPPE", line: "HVL", value: "Upper Hutt", rank: "18" },
    { station: "WELL", line: "KPL", value: "Wellington", rank: "1" },
    { station: "TAKA", line: "KPL", value: "Takapu Rd", rank: "3" },
    { station: "REDW", line: "KPL", value: "Redwood", rank: "4" },
    { station: "TAWA", line: "KPL", value: "Tawa", rank: "5" },
    { station: "LIND", line: "KPL", value: "Linden", rank: "6" },
    { station: "KENE", line: "KPL", value: "Kenepuru", rank: "7" },
    { station: "PORI", line: "KPL", value: "Porirua", rank: "8" },
    { station: "PARE", line: "KPL", value: "Paremata", rank: "9" },
    { station: "MANA", line: "KPL", value: "Mana", rank: "10" },
    { station: "PLIM", line: "KPL", value: "Plimmerton", rank: "11" },
    { station: "PUKE", line: "KPL", value: "Pukerua Bay", rank: "12" },
    { station: "PAEK", line: "KPL", value: "Paekakariki", rank: "13" },
    { station: "PARA", line: "KPL", value: "Paraparaumu", rank: "14" },
    { station: "WAIK", line: "KPL", value: "Waikanae", rank: "15" },
    { station: "WELL", line: "MEL", value: "Wellington", rank: "1" },
    { station: "NGAU", line: "MEL", value: "Ngauranga", rank: "3" },
    { station: "PETO", line: "MEL", value: "Petone", rank: "4" },
    { station: "WEST", line: "MEL", value: "Western Hutt", rank:"5" },
    { station: "MELL", line: "MEL", value: "Melling", rank: "6" },
    { station: "WELL", line: "WRL", value: "Wellington", rank: "1" },
    { station: "PETO", line: "WRL", value: "Petone", rank: "4" },
    { station: "WATE", line: "WRL", value: "Waterloo", rank: "7" },
    { station: "UPPE", line: "WRL", value: "Upper Hutt", rank: "18" },
    { station: "MAYM", line: "WRL", value: "Maymorn", rank: "19" },
    { station: "FEAT", line: "WRL", value: "Featherston", rank: "20" },
    { station: "WOOD", line: "WRL", value: "Woodside", rank: "21" },
    { station: "MATA", line: "WRL", value: "Matarawa", rank: "22" },
    { station: "CART", line: "WRL", value: "Carterton", rank: "23" },
    { station: "SOLW", line: "WRL", value: "Solway", rank: "24" },
    { station: "RENA", line: "WRL", value: "Renall St", rank: "25" },
    { station: "MAST", line: "WRL", value: "Masterton", rank: "26" }
];

function addOptionToSelect(selectToAddTo, Value, Name) {
	var option=document.createElement("option");
	option.text=Name;
	option.value=Value;
	try {
		// for IE earlier than version 8
	  	selectToAddTo.add(option,x.options[null]);
	 }
	catch (e) {
	  	selectToAddTo.add(option,null);
	}
}

function clearSelectList(selectIdToClear) {
	selectIdToClear.innerHTML = "";
}

function setupStations() {

	var selectedLine = linesList.options[linesList.selectedIndex].value;

	clearSelectList(fromList);
	clearSelectList(toList);

	for (var i = 0; i < stationLines.length; i += 1) {
		if (stationLines[i].line == selectedLine) {
			addOptionToSelect(fromList, stationLines[i].station, stationLines[i].value);
			addOptionToSelect(toList, stationLines[i].station, stationLines[i].value);
		}
	}
	toList[1].selected = true;
}

function swapDirections() {
	var temp = toList.selectedIndex;
	toList[fromList.selectedIndex].selected = true;
	fromList[temp].selected = true;
}

function findTrains() {
	var temp = toList.selectedIndex;
	toList[fromList.selectedIndex].selected = true;
	fromList[temp].selected = true;
}

(function() {
		var test = document.getElementById("lineSelection");
		for (var i = 0; i < linesList.length; i += 1) {
			if (linesList[i].value == test.value) {
				linesList[i].selected = true;
			}
		}
		setupStations();
		test = document.getElementById("fromSelection");
		for (var i = 0; i < fromList.length; i += 1) {
			if (fromList[i].value == test.value) {
				fromList[i].selected = true;
			}
		}
		test = document.getElementById("toSelection");
		for (var i = 0; i < toList.length; i += 1) {
			if (toList[i].value == test.value) {
				toList[i].selected = true;
			}
		}
		test = document.getElementById("daySelection");
		for (var i = 0; i < dayList.length; i += 1) {
			if (dayList[i].value == test.value) {
				dayList[i].selected = true;
			}
		}
})();
	