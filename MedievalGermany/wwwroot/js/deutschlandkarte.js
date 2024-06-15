export async function InitMap() {
    var dk = document.getElementById("deutschland-karte");

    var map = L.map(dk, {
        zoomSnap: 0.1,
    }).setView([51.3, 9.3], 5.5);

    // Kartenmaterial laden
    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        minZoom: 5,
        maxZoom: 16,
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright" target="_blank">OpenStreetMap</a> contributors'
    }).addTo(map);

    // Initialisierte Karte dem Element hinzufügen, damit wir im Code weiter unten darauf zugreifen können.
    dk.MapInstance = map;
}

export async function CreateMarker() {
    var dk = document.getElementById("deutschland-karte");
    if (dk === null) {
        return;
    }

    var map = dk.MapInstance;
    if (map == null) {
        return;
    }


}

