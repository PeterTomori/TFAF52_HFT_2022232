let companies = [];
let planets = [];
let ships = [];
let factioncounted = [];

let connection = null;

let companyIdToUpdate = -1;
let planetIdToUpdate = -1;
let shipIdToUpdate = -1;

setupSignalR();
getcompanydata();
getplanetdata();
getshipdata();
getshipfactions();
getshipmanufacturers();

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
    await fetch('http://localhost:27110/planet')
        .then(x => x.json())
        .then(y => {
            planets = y;
            planetdisplay();
        });
}

async function getshipdata() {
    await fetch('http://localhost:27110/ship')
        .then(x => x.json())
        .then(y => {
            ships = y;
            shipdisplay();
        });
}

//Non-crud

async function getshipfactions() {
    await fetch('http://localhost:27110/stat/shipFactions/')
        .then(x => x.json())
        .then(y => {
            factioncounted = y;
            shipfactionsdisplay();
        });
}

async function getshipmanufacturers() {
    const shipName = document.getElementById('shipnameget').value;
    await fetch('http://localhost:27110/stat/shipmanufacturers/' + shipName)
        .then(x => x.json())
        .then(y => {
            companies = y;
            shipmanufacturersdisplay();
        });
}

async function getshipoffactions() {
    let factionName = document.getElementById('factionget').value;
    await fetch('http://localhost:27110/stat/shipOfFactions/' + factionName)
        .then(x => x.json())
        .then(y => {
            ships = y;
            shipoffactionsdisplay();
        });
}

async function getownedbycompany() {
    let companyName = document.getElementById('companyget').value;
    await fetch('http://localhost:27110/stat/ownedByCompany/' + companyName)
        .then(x => x.json())
        .then(y => {
            ships = y;
            ownedbycompanydisplay();
        });
}

async function getownerofplanet() {
    const planetName = document.getElementById('planetget').value;
    await fetch('http://localhost:27110/stat/ownerOfPlanet/' + planetName)
        .then(x => x.json())
        .then(y => {
            companies = y;
            ownerofplanetdisplay();
        });
}

//Crud Displays

function companydisplay() {
    document.getElementById('companyresultarea').innerHTML = "";
    companies.forEach(t => {
        document.getElementById('companyresultarea').innerHTML +=
            "<tr><td>" + t.companyId + "</td><td>"
        + t.companyName + "</td><td>"
        + t.faction + "</td><td>" +
        `<button type="button" onclick="companyremove(${t.companyId})">Delete</button>` +
        `<button type="button" onclick="companyshowupdate(${t.companyId})">Update</button>` + "</td></tr>";
        console.log(t.companyName);
    });
}

function planetdisplay() {
    document.getElementById('planetresultarea').innerHTML = "";
    planets.forEach(t => {
        document.getElementById('planetresultarea').innerHTML +=
            "<tr><td>" + t.planetId + "</td><td>" + t.planetName + "</td><td>" +
            //t.companyId + "</td><td>" +
            `<button type="button" onclick="planetremove(${t.planetId})">Delete</button>` +
            `<button type="button" onclick="planetshowupdate(${t.planetId})">Update</button>` + "</td></tr>";
        console.log(t.planetName);
    });
}

function shipdisplay() {
    document.getElementById('shipresultarea').innerHTML = "";
    ships.forEach(t => {
        document.getElementById('shipresultarea').innerHTML +=
            "<tr><td>" + t.shipId + "</td><td>" + t.shipName + "</td><td>" + t.shipType + "</td><td>" +
            //t.companyId + "</td><td>" +
            `<button type="button" onclick="shipremove(${t.shipId})">Delete</button>` +
            `<button type="button" onclick="shipshowupdate(${t.shipId})">Update</button>` + "</td></tr>";
        console.log(t.shipName);
    });
}

//Non-Crud Displays

function shipfactionsdisplay() {
    document.getElementById('shipfactionsresultarea').innerHTML = "";
    factioncounted.forEach(t => {
        document.getElementById('shipfactionsresultarea').innerHTML +=
            "<tr><td>" + t.faction + "</td><td>" + t.shipCount + "</td></tr>";
        console.log(t.faction);
    });
}

function shipmanufacturersdisplay() {
    document.getElementById('shipmanufacturersresultarea').innerHTML = "";
    companies.forEach(t => {
        document.getElementById('shipmanufacturersresultarea').innerHTML +=
            "<tr><td>" + t.companyId + "</td><td>"
            + t.companyName + "</td><td>"
            + t.faction + "</td></tr>";
        console.log(t.companyName);
    });
}

function shipoffactionsdisplay() {
    document.getElementById('shipoffactionsresultarea').innerHTML = "";
    ships.forEach(t => {
        document.getElementById('shipoffactionsresultarea').innerHTML +=
            "<tr><td>" + t.shipId + "</td><td>"
            + t.shipName + "</td><td>"
            + t.shipType + "</td></tr>";
        console.log(t.shipName);
    });
}

function ownedbycompanydisplay() {
    document.getElementById('ownedbycompanyresultarea').innerHTML = "";
    ships.forEach(t => {
        document.getElementById('ownedbycompanyresultarea').innerHTML +=
            "<tr><td>" + t.planetId + "</td><td>"
            + t.planetName + "</td></tr>";
        console.log(t.shipName);
    });
}

function ownerofplanetdisplay() {
    document.getElementById('ownerofplanetresultarea').innerHTML = "";
    companies.forEach(t => {
        document.getElementById('ownerofplanetresultarea').innerHTML +=
            "<tr><td>" + t.companyId + "</td><td>"
            + t.companyName + "</td><td>"
            + t.faction + "</td></tr>"
        console.log(t.companyName);
    });
}



//Show Updates

function companyshowupdate(id) {
    document.getElementById('companynametoupdate').value = companies.find(t => t['companyId'] == id)['companyName'];
    document.getElementById('factiontoupdate').value = companies.find(t => t['companyId'] == id)['faction'];
    document.getElementById('companyupdateformdiv').style.display = 'flex';
    companyIdToUpdate = id;
}

function planetshowupdate(id) {
    document.getElementById('planetnametoupdate').value = planets.find(t => t['planetId'] == id)['planetName'];
    document.getElementById('planetupdateformdiv').style.display = 'flex';
    planetIdToUpdate = id;
}

function shipshowupdate(id) {
    document.getElementById('shipnametoupdate').value = ships.find(t => t['shipId'] == id)['shipName'];
    document.getElementById('shiptypetoupdate').value = ships.find(t => t['shipId'] == id)['shipType'];
    /*document.getElementById('shipcompanyidtoupdate').value = ships.find(t => t['shipId'] == id['companyId']);*/
    document.getElementById('shipupdateformdiv').style.display = 'flex';
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
    let faction = document.getElementById('factiontoupdate').value;
    fetch('http://localhost:27110/company', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                companyName: name,
                faction: faction,
                companyId: companyIdToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            companygetdata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('companyupdateformdiv').style.display = 'none';
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

//Planet Actions

function planetcreate() {
    let name = document.getElementById('planetname').value;
    fetch('http://localhost:27110/planet', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                planetName: name
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            planetgetdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function planetupdate() {
    let name = document.getElementById('planetnametoupdate').value;
    fetch('http://localhost:27110/planet', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                planetName: name,
                planetId: planetIdToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            planetgetdata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('planetupdateformdiv').style.display = 'none';
}

function planetremove(id) {
    fetch('http://localhost:27110/planet/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            planetgetdata();
        })
        .catch((error) => { console.error('Error:', error); })
}

//Ship Actions

function shipcreate() {
    let name = document.getElementById('shipname').value;
    let type = document.getElementById('shiptype').value;
    fetch('http://localhost:27110/ship', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                shipName: name,
                shipType: type
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            shipgetdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function shipupdate() {
    let name = document.getElementById('shipnametoupdate').value;
    let type = document.getElementById('shiptypetoupdate').value;
    //let id = document.getElementById('shipcompanyidtoupdate').value;
    fetch('http://localhost:27110/ship', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                shipName: name,
                shipType: type,
                //shipCompanyId: id,
                shipId: shipIdToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            shipgetdata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('shipupdateformdiv').style.display = 'none';
}

function shipremove(id) {
    fetch('http://localhost:27110/ship/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            shipgetdata();
        })
        .catch((error) => { console.error('Error:', error); })
}
