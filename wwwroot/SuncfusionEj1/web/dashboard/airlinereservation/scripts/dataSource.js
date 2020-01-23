function GetData() {
    var dataArray = [];
    var airwaysProvidersList = GerAirProvidersList();
    for (var cnt = 0; cnt < airwaysProvidersList.length; cnt++) {

        var citiesLst = GetCitiesList();
        var fromIndex = GetRandCityIndex(GetCitiesList().length - 1);
        var toIndex = GetRandCityIndex(GetCitiesList().length - 1);
        if (fromIndex == toIndex) {
            toIndex = (toIndex + 1) > citiesLst.Count - 1 ? 0 : toIndex + 1;
        }

        var Airline = airwaysProvidersList[cnt];
        var From = citiesLst[fromIndex];
        var To = citiesLst[toIndex];
        var Depart = GetTime();
        var Arrive = GetTime().toFixed(2);
        var Price = GetPrice().toFixed(2);
        var Rating = Math.floor(Math.random() * 5);

        dataArray.push({
            Airline: Airline.flightName,
            From: From.cityName,
            To: To.cityName,
            Depart: Depart,
            Arrive: Arrive,
            Price: Price,
            Rating: Rating
        });
    }
    return dataArray;
}
function GerAirProvidersList() {
    var flightlist = [
    { flightName: "Aero Flot" },
    { flightName: "Aero Mexico" },
    { flightName: "Air NewZealand" },
    { flightName: "AirBerlin" },
    { flightName: "AirCanada" },
    { flightName: "AirFrance" },
    { flightName: "AirIndia" },
    { flightName: "AirMadagascar" },
    { flightName: "AirPhilipines" },
    { flightName: "AirTran" },
    { flightName: "AlaskaAirlines" },
    { flightName: "Alitalia" },
    { flightName: "Austrian" },
    { flightName: "Avianca" },
    { flightName: "British Airways" },
    { flightName: "Brussels Airlines" },
    { flightName: "CathayPacific" },
    { flightName: "China Airlines" },
    { flightName: "Continental Airlines" },
    { flightName: "Croatia Airlines" },
    { flightName: "Dragonair" },
    { flightName: "Delta" },
    { flightName: "Elal" },
    { flightName: "Emirates" },
    { flightName: "Ethiopian" },
    { flightName: "Garuda Indonesia" },
    { flightName: "Hawaiian" },
    { flightName: "Iberia" },
    { flightName: "IceLandAir" },
    { flightName: "Jal" },
    { flightName: "KLM" },
    { flightName: "KoreanAir" },
    { flightName: "Lan" },
    { flightName: "Lot" },
    { flightName: "Lufthansa" },
    { flightName: "Malaysia" },
    { flightName: "MidWest Airlines" },
    { flightName: "NWA" },
    { flightName: "Oceanic Airlines" },
    { flightName: "Qantas" },
    { flightName: "Sabena" },
    { flightName: "Singapore Airlines" },
    { flightName: "SouthAfrican Airways" },
    { flightName: "Spirit Airlines" },
    { flightName: "SriLankan Airlines" },
    { flightName: "SwissAir" },
    { flightName: "Tap" },
    { flightName: "Thai" },
    { flightName: "Turkish AirLines" },
    { flightName: "United Airlines" },
    { flightName: "Varig" },
    { flightName: "Vietnam Airlines" },
    { flightName: "Wideroe" }
    ];
    return flightlist;
}

function GetCitiesList() {
    var cities = [
        { cityName: "Atlanta" },
        { cityName: "London" },
        { cityName: "Los Angeles" },
        { cityName: "Dallas" },
        { cityName: "Alexandria" },
        { cityName: "Paris" },
        { cityName: "Amsterdam" },
        { cityName: "Danver" },
        { cityName: "Madrid" },
        { cityName: "Houston" },
        { cityName: "HongKong" },
        { cityName: "Minneapolis" },
        { cityName: "Detroit" },
        { cityName: "Bangkok" },
        { cityName: "SanFransisco" },
        { cityName: "Bandon" },
        { cityName: "Miami" },
        { cityName: "New york" },
        { cityName: "Singapore" },
        { cityName: "Tokyo" },
        { cityName: "Beijing" },
        { cityName: "Seattle" },
        { cityName: "Belize City" },
        { cityName: "Orlando" },
        { cityName: "Berlin" },
        { cityName: "Bishop" },
        { cityName: "Toronto" },
        { cityName: "Brownwood" },
        { cityName: "Saint Louis" },
        { cityName: "Chicago" },
        { cityName: "FrankFurt" },
        { cityName: "Madrid" },
        { cityName: "Las Vegas" },
        { cityName: "Phoenix" },
        { cityName: "Delhi" },
        { cityName: "Dubai" },
        { cityName: "Newark" },
        { cityName: "ROME" },
        { cityName: "Charlotte" },
        { cityName: "Munich" },
        { cityName: "Guangzhou" },
        { cityName: "Eureka" },
        { cityName: "Sydney" },
        { cityName: "Jakarta" },
        { cityName: "Philadelphia" },
        { cityName: "Barcelona" },
        { cityName: "Incheon" },
        { cityName: "Istanbul" },
        { cityName: "Shangai" },
        { cityName: "Kuala Lumpur" },
        { cityName: "Mexico" },
        { cityName: "Boston" },
        { cityName: "Melbourne" },
        { cityName: "Mumbai" },
        { cityName: "Dublin" },
        { cityName: "Palma De Mallorca" },
        { cityName: "Fort Lauderdale" },
        { cityName: "Zurich" },
        { cityName: "Manaila" },
        { cityName: "Taipei" },
        { cityName: "Copenhagen" },
        { cityName: "Manchester" },
        { cityName: "Shenzhen" },
        { cityName: "Sao paulo" },
        { cityName: "Baltimore MD" },
        { cityName: "Salt Lake City" },
        { cityName: "Moscow" },
        { cityName: "Vienna" },
        { cityName: "Oslo" },
        { cityName: "Millan" },
        { cityName: "Brisbane" },
        { cityName: "Antalya" },
        { cityName: "Honolulu" },
        { cityName: "Johannesburg" },
        { cityName: "Brussels" },
        { cityName: "Tampa FL" },
        { cityName: "Stockholm" },
        { cityName: "Dusseldorf" },
        { cityName: "San Diego CA" },
        { cityName: "Vancouver" },
        { cityName: "Sapporo" },
        { cityName: "Washington" },
        { cityName: "Fukuoka" },
        { cityName: "Chengdu" },
        { cityName: "Athens" },
        { cityName: "Osaka" },
        { cityName: "Jeddah" },
        { cityName: "Naha" },
        { cityName: "St Louis MO" },
        { cityName: "Cairo" },
        { cityName: "Portland" },
        { cityName: "Seoul" },
        { cityName: "CinCinnati OH" },
        { cityName: "Lisbon" },
        { cityName: "Bogota" },
        { cityName: "Helsinki" },
        { cityName: "Hall Beach" },
        { cityName: "Eagle" },
        { cityName: "Easton" },
        { cityName: "Elkedra" },
        { cityName: "Finke" },
        { cityName: "Florenceh" },
        { cityName: "Franklin" },
        { cityName: "Fuyang" },
        { cityName: "Gangaw" },
        { cityName: "Gangneung" },
        { cityName: "Goa" },
        { cityName: "Greenville" },
        { cityName: "Indiana" },
        { cityName: "Inverway" },
        { cityName: "Itumbiara" },
        { cityName: "Kalgoorlieh" },
        { cityName: "Kambuaya" },
        { cityName: "Kasba Lake" },
        { cityName: "Kenora" },
        { cityName: "Kingston" },
        { cityName: "Queretaro" },
        { cityName: "Qiemo" },
        { cityName: "Quillayute" },
        { cityName: "Queenstown" },
        { cityName: "Quanduc" },
        { cityName: "Rafha" },
        { cityName: "Rajshahi" },
        { cityName: "Ramadan" },
        { cityName: "Ranong" },
        { cityName: "Redencao" },
        { cityName: "Richmond" },
        { cityName: "Ube" },
        { cityName: "Udine" },
        { cityName: "Union City" },
        { cityName: "Ulusaba" },
        { cityName: "Upland" },
        { cityName: "Valentine" },
        { cityName: "Vaasa" },
        { cityName: "Vadodara" },
        { cityName: "Varanasi" },
        { cityName: "Vejle" },
        { cityName: "Xiangfan" },
        { cityName: "Xingcheng" },
        { cityName: "Xining" },
        { cityName: "Xinguara" },
        { cityName: "Xayabury" },
        { cityName: "Yaroslavl" },
        { cityName: "Yeovilton" },
        { cityName: "Yonago" },
        { cityName: "Yorketown" },
        { cityName: "Zambezi" },
        { cityName: "Zahedan" },
        { cityName: "Zacatecas" },
        { cityName: "Zephyrhills" },
        { cityName: "Chennai" }
    ];
    return cities;
}

function GetRandCityIndex(count) {
    return Math.floor(Math.random() * count);
}

function GetPrice() {
    return Math.floor(Math.random() * (5000 - 400 + 1)) + 400;
}

function GetTime() {
    var time = Math.floor((Math.random() * 23) + 1);
    var minutes = Math.floor(Math.random() * (59 - 0 + 1)) + 0;
    var cc = time + (minutes / 100);
    return cc;
}
