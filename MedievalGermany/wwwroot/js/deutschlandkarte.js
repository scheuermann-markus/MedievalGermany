export async function InitMap(dotNetObject) {
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

    // EventListener auf map setzten
    map.on('moveend', () => {
        OnBoundingBoxChanged(map, dotNetObject);
    });

    // Initialisierte Karte dem Element hinzufügen, damit wir im Code weiter unten darauf zugreifen können.
    dk.MapInstance = map;
}

export async function CreateMarker(castles) {
    var dk = document.getElementById("deutschland-karte");
    if (dk === null) {
        return;
    }

    var map = dk.MapInstance;
    if (map == null) {
        return;
    }

    // Marker Cluster Group und alle Marker entfernen
    if (dk.CurrentMarkerClusterGroup != null) {
        map.removeLayer(dk.CurrentMarkerClusterGroup); 
    }

    var markerClusterGroup = GetMarkerClusterGroup();

    var allMarkers = await GetMarkers(castles);
    allMarkers.forEach((marker) => {
        markerClusterGroup.addLayer(marker);
    });

    // Add Marker Cluster Group to the map
    map.addLayer(markerClusterGroup);
    // Add Marker Cluster Group to the element
    dk.CurrentMarkerClusterGroup = markerClusterGroup;
}


const GetMarkerClusterGroup = () => {
    return (
        L.markerClusterGroup({
            showCoverageOnHover: true,
            removeOutsideVisibleBounds: true, // Clusters and markers too far from the viewport are removed from the map for performance
            iconCreateFunction: function (cluster) {
                return L.divIcon({
                    html: '<b>' + cluster.getChildCount() + '</b>',
                    className: 'deutschland-karte__cluster',
                    iconSize: L.point(40, 40)
                });
            }
        })
    );
}


const GetMarkers = async (castles) => {
    var allMarkers = [];
    let markerColor = "#38697e";

    for (let i = 0; i < castles.length; i++) {
        let name = castles[i].name;

        let marker = L.marker([castles[i].geolocation.latitude, castles[i].geolocation.longitude], {
            // Customize Icon
            icon: L.divIcon({
                iconSize: [24, 40],
                iconAnchor: [12, 40],
                className: "deutschlandkarte__icon",
                html: '<svg version="1.0" xmlns="http://www.w3.org/2000/svg" width="40.000000pt" height="40.000000pt" viewBox="0 0 360.000000 360.000000" preserveAspectRatio="xMidYMid meet"> <g transform="translate(0.000000, 260.000000) scale(0.100000,-0.100000)" fill="' + markerColor + '" stroke="none"> <path d="M657 2335 c-270 -51 -474 -231 -563 -495 -27 -78 -29 -96 -29 -230 1 -130 3 -153 28 -225 35 -105 87 -193 167 -279 219 -239 373 -520 475 -870 20 -65 38 -136 41 -157 9 -56 21 -56 29 -1 10 69 69 260 122 392 89 226 211 427 358 591 147 164 212 294 235 472 27 199 -51 434 -194 585 -163 175 -431 261 -669 217z m233 -476 c60 -27 88 -54 116 -114 44 -94 24 -216 -48 -282 -90 -84 -264 -80 -345 8 -84 92 -80 266 8 345 73 65 182 83 269 43z"/></g></svg>'
            }),
        });

        allMarkers[i] = marker;
    }

    return allMarkers;
}

const OnBoundingBoxChanged = (map, dotNetObject) => {
    let currentMapView = map.getBounds(); 

    if (currentMapView._southWest.lat == currentMapView._northEast.lat && currentMapView._southWest.lng == currentMapView._northEast.lng) {
        // Bounds ungültig... nicht an Dotnet weitergeben
        return;
    }

    dotNetObject.invokeMethodAsync('BoundingBoxChanged', currentMapView._southWest.lat, currentMapView._southWest.lng, currentMapView._northEast.lat, currentMapView._northEast.lng);
}