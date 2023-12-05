let companies = [];
let planets = [];
let ships = [];

let connection = null;

let companyIdToUpdate = -1;
let planetIdToUpdate = -1;
let shipIdToUpdate = -1;

getcompanydata();
setupSignalR();
getplanetdata();
getshipdata();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27110/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CompanyCreated", (user, message) => {
        getcompanydata();
    });

    connection.on("CompanyDeleted", (user, message) => {
        getcompanydata();
    });

    connection.on("CompanyUpdated", (user, message) => {
        getcompanydata();
    });

    connection.on("PlanetCreated", (user, message) => {
        getplanetdata();
    });

    connection.on("PlanetDeleted", (user, message) => {
        getplanetdata();
    });

    connection.on("PlanetUpdated", (user, message) => {
        getplanetdata();
    });

    connection.on("ShipCreated", (user, message) => {
        getshipdata();
    });

    connection.on("ShipDeleted", (user, message) => {
        getshipdata();
    });

    connection.on("ShipUpdated", (user, message) => {
        getshipdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getcompanydata() {
    await fetch('http://localhost:27110/company')
        .then(x => x.json())
        .then(y => {
            companies = y;
            companydisplay();
        });
}

async function getplanetdata() {
    await fetch('http://localhost:27110/company')
        .then(x => x.json())
        .then(y => {
            planets = y;
            planetdisplay();
        });
}

async function getshipdata() {
    await fetch('http://localhost:27110/company')
        .then(x => x.json())
        .then(y => {
            ships = y;
            shipdisplay();
        });
}

function companydisplay() {
    document.getElementById('companyresultarea').innerHTML = "";
    companies.forEach(t => {
        document.getElementById('companyresultarea').innerHTML +=
        "<tr><td>" + t.companyId + "</td><td>" + t.companyName + "</td><td>" + t.faction +
            "</td><td>" + `<button type="button" onclick="remove(${t.companyId})">Delete</button>` +
        `<button type="button" onclick="companyshowupdate(${t.companyId})">Update</button>` + "</td></tr>";
        console.log(t.companyName);
    });
}

function planetdisplay() {
    document.getElementById('planetresultarea').innerHTML = "";
    planets.forEach(t => {
        document.getElementById('planetresultarea').innerHTML +=
            "<tr><td>" + t.planetId + "</td><td>" + t.planetName + "</td><td>" + t.companyId + "</td><td>" +
            `<button type="button" onclick="remove(${t.planetId})">Delete</button>` +
            `<button type="button" onclick="planetshowupdate(${t.planetId})">Update</button>` + "</td></tr>";
        console.log(t.planetName);
    });
}

function shipdisplay() {
    document.getElementById('shipresultarea').innerHTML = "";
    ships.forEach(t => {
        document.getElementById('shipresultarea').innerHTML +=
            "<tr><td>" + t.shipId + "</td><td>" + t.shipName + "</td><td>" + t.shipType + "</td><td>" + t.companyId +
            "</td><td>" + `<button type="button" onclick="remove(${t.shipId})">Delete</button>` +
            `<button type="button" onclick="shipshowupdate(${t.shipId})">Update</button>` + "</td></tr>";
        console.log(t.shipName);
    });
}

function companyshowupdate(id) {
    document.getElementById('companynametoupdate').value = companies.find(t => t['companyId'] == id)['companyName'];
    document.getElementById('faction').value = companies.find(t => t['companyId'] == id)['faction'];
    document.getElementById('updateformdiv').style.display = 'flex';
    companyIdToUpdate = id;
}

function planetshowupdate(id) {
    document.getElementById('planetnametoupdate').value = planets.find(t => t['planetId'] == id)['planetName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    planetIdToUpdate = id;
}

function shipshowupdate(id) {
    document.getElementById('shipnametoupdate').value = ships.find(t => t['shipId'] == id)['shipName'];
    document.getElementById('shiptypetoupdate').value = ships.find(t => t['shipId'] == id)['shipType'];
    document.getElementById('updateformdiv').style.display = 'flex';
    shipIdToUpdate = id;
}

//Company Actions

function companycreate() {
    let name = document.getElementById('companyname').value;
    let faction = document.getElementById('faction').value;
    fetch('http://localhost:27110/company', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                companyName: name,
                faction: faction
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            companygetdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function companyupdate() {
    let name = document.getElementById('companynametoupdate').value;
    fetch('http://localhost:27110/company', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                companyName: name,
                companyId: companyIdToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            companygetdata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('updateformdiv').style.display = 'none';
}

function companyremove(id) {
    fetch('http://localhost:27110/company/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            companygetdata();
        })
        .catch((error) => { console.error('Error:', error); })
}