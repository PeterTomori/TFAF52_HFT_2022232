let companies = [];
let connection = null;

let companyIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27110/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CompanyCreated", (user, message) => {
        getdata();
    });

    connection.on("CompanyDeleted", (user, message) => {
        getdata();
    });

    connection.on("CompanyUpdated", (user, message) => {
        getdata();
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

async function getdata() {
    await fetch('http://localhost:27110/company')
        .then(x => x.json())
        .then(y => {
            companies = y;
            //console.log(companies);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    companies.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
        "<tr><td>" + t.companyId + "</td><td>" + t.companyName + "</td><td>" + t.faction +
            "</td><td>" + `<button type="button" onclick="remove(${t.companyId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.companyId})">Update</button>` + "</td></tr>";
        console.log(t.companyName);
    });
}

function remove(id) {
    fetch('http://localhost:27110/company/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); })
}

function showupdate(id) {
    document.getElementById('companynametoupdate').value = companies.find(t => t['companyId'] == id)['companyName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    companyIdToUpdate = id;
}

function udpate() {
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
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('updateformdiv').style.display = 'none';
}

function create() {
    let name = document.getElementById('companyname').value;
    fetch('http://localhost:27110/company', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { companyName: name }),
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
        
}