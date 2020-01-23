window.departmentName = [
	{ Name: "GENERAL", Text: "GENERAL", Id: 1, GroupId: 10, Color: "#C3DAF1" },
    { Name: "DENTAL", Text: "DENTAL", Id: 2, GroupId: 10, Color: "#FBDBD8" }
];
window.doctorName = [
	{ Name: "John Xavier", Text: "JohnXavier", Id: 1, GroupId: 1, Color: "#cb6bb2", Designation: "Cardiologist", Value: "cardiologist" },
    { Name: "Annie", Text: "Annie", Id: 2, GroupId: 2, Color: "#56ca85", Designation: "Oncologist", Value: "oncologist" },
    { Name: "Paul",  Text: "Paul", Id: 3, GroupId: 1, Color: "#cb6bb2", Designation: "Ophthalmologist", Value: "ophthalmologist" },
    { Name: "Helen", Text: "Helen", Id: 4, GroupId: 2, Color: "#56ca85", Designation: "Orthopedic", Value: "orthopedic" },
    { Name: "Smith", Text: "Smith", Id: 5, GroupId: 1, Color: "#cb6bb2", Designation: "Neurologist", Value: "neurologist" },
    { Name: "George", Text: "George", Id: 6, GroupId: 2, Color: "#56ca85", Designation: "Child Specialist", Value: "childspecialist" },
    { Name: "Benita", Text: "Benita", Id: 7, GroupId: 1, Color: "#cb6bb2", Designation: "Dermatologist", Value: "dermatologist" },
    /* { Name: "Will Smith", Text: "WillSmith", Id: 8, GroupId: 1, Color: "#cb6bb2", Designation: "Urologist", Value: "urologist" }, */
    /* { Name: "Nancy", Text: "Nancy", Id: 9, GroupId: 1, Color: "#cb6bb2", Designation: "Perinatologist", Value: "perinatologist" }, */
    /* { Name: "Shirley", Text: "Shirley", Id: 10, GroupId: 2, Color: "#56ca85", Designation: "Hematologist", Value: "hematologist" } */
];
window.categorylist = [
	{ Text: "Consulting", Value: "consulting", Color: "#9578b3" }, 
	{ Text: "Check up", Value: "checkup", Color: "#64b7a0" }, 
	{ Text: "Extraction", Value: "extraction", Color: "#c95d5d" }, 
	{ Text: "Observation", Value: "observation", Color: "#8196a7" }, 
	{ Text: "Surgery", Value: "surgery", Color: "#6a8fcb" }, 
	{ Text: "Therapy", Value: "therapy", Color: "#5B7D38" }
];
window.patientlist = [
	{ Id: 1, Name: "David", DOB: "5/29/1991", Mobile: 9897969594, BloodGroup: "A+", Address: "Chennai" }, 
	{ Id: 2, Name: "John", DOB: "6/8/1991", Mobile: 9897969594, BloodGroup: "A-", Address: "Chennai" }, 
	{ Id: 3, Name: "Peter", DOB: "10/10/1991", Mobile: 9897969594, BloodGroup: "AB+", Address: "Chennai" }, 
	{ Id: 4, Name: "Starc", DOB: "10/30/1991", Mobile: 9897969594, BloodGroup: "AB-", Address: "Chennai" }, 
	{ Id: 5, Name: "James", DOB: "10/26/1991", Mobile: 9897969594, BloodGroup: "O+", Address: "Chennai" }, 
	{ Id: 6, Name: "Jercy", DOB: "7/18/1991", Mobile: 9897969594, BloodGroup: "O+", Address: "Chennai" }, 
	{ Id: 7, Name: "Albret", DOB: "4/1/1991", Mobile: 9897969594, BloodGroup: "O+", Address: "Chennai" }, 
	{ Id: 8, Name: "Robert", DOB: "10/29/1991", Mobile: 9897969594, BloodGroup: "A-", Address: "Chennai" }, 
	{ Id: 9, Name: "Louis", DOB: "5/17/1991", Mobile: 9897969594, BloodGroup: "A-", Address: "Chennai" }, 
	{ Id: 10, Name: "Williams", DOB: "5/25/1991", Mobile: 9897969594, BloodGroup: "A+", Address: "Chennai" }
];
window.hospitaldata = [
{ 
	Id: 1, 
	Subject: "David", 
	StartTime: new Date(2016,3,01,15,00), 
	EndTime: new Date(2016,3,01,17,00), 
	Description: "Health Checkup", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 1, 
	OwnerId: 1, 
	Category: "consulting" 
}, { 
	Id: 2, 
	Subject: "John", 
	StartTime: new Date(2016,3,01,16,30), 
	EndTime: new Date(2016,3,01,18,30), 
	Description: "Monthly Treatment", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 2, 
	OwnerId: 2, 
	Category: "checkup" 
}, { 
	Id: 3, 
	Subject: "Peter", 
	StartTime: new Date(2016,3,01,19,00), 
	EndTime: new Date(2016,3,01,21,00), 
	Description: "Eye and Spectacles Checkup", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 1, 
	OwnerId: 3, 
	Category: "extraction" 
}, { 
	Id: 4, 
	Subject: "Starc", 
	StartTime: new Date(2016,3,01,19,00), 
	EndTime: new Date(2016,3,01,21,00), 
	Description: "Bone and Health Checkup ", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 2, 
	OwnerId: 4, 
	Category: "observation" 
}, {
    Id: 5,
    Subject: "James",
    StartTime:  new Date(2016,3,01,22,00),
    EndTime:  new Date(2016,3,02,00,00),
	Description:"Surgery Appointment",
    AllDay: false,
    Recurrence: false,
    RoomId: 1, 
	OwnerId: 5,
	Category: "therapy"
}, {
    Id: 6,
    Subject: "Jercy",
    StartTime:  new Date(2016,3,01,21,30),
    EndTime: new Date(2016,3,01,23,30),
	Description:"Monthly Checkup for baby",
    AllDay: false,
    Recurrence: false,
	RoomId: 2, 
	OwnerId: 6,
	Category: "surgery"
}, {
    Id: 7,
    Subject: "Albert",
    StartTime:  new Date(2016,3,02,00,00),
    EndTime:  new Date(2016,3,02,02,30),
	Description:"Skin care treatment",
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=10",
    RoomId: 1, 
	OwnerId: 7,
	Category: "extraction"
}, 
//{
    // Id: 8,
    // Subject: "Robert",
    // StartTime:  new Date(2016,3,02,03,30),
    // EndTime:  new Date(2016,3,02,05,30),
	// Description:"Complete checkup after surgery",
    // AllDay: false,
    // Recurrence: true,
    // RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=10",
	// RoomId: 1, 
	// OwnerId: 8,
	// Category: "consulting"
// }, {
    // Id: 9,
    // Subject: "Louis",
    // StartTime:  new Date(2016,3,02,01,30),
    // EndTime: new Date(2016,3,02,03,45),
	// Description:"General Checkup",
    // AllDay: false,
    // Recurrence: false,
    // RoomId: 1, 
	// OwnerId: 9,
	// Category: "therapy"
// }, { 
	// Id: 10, 
	// Subject: "Williams", 
	// StartTime: new Date(2016,3,02,02,00), 
	// EndTime: new Date(2016,3,02,04,00), 
	// Description: "Master Checkup", 
	// AllDay: false, 
	// Recurrence: false, 
	// RoomId: 2, 
	// OwnerId: 10, 
	// Category: "consulting" 
// }, 
{ 
	Id: 11, 
	Subject: "David", 
	StartTime: new Date(2016,3,02,16,30), 
	EndTime: new Date(2016,3,02,18,15), 
	Description: "Eye checkup and Treatment", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 1, 
	OwnerId: 1, 
	Category: "consulting" 
}, { 
	Id: 12, 
	Subject: "John", 
	StartTime: new Date(2016,3,02,19,30), 
	EndTime: new Date(2016,3,02,21,45), 
	Description: "Skin Checkup and Treatment", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 2, 
	OwnerId: 2, 
	Category: "checkup" 
}, { 
	Id: 13, 
	Subject: "Peter", 
	StartTime: new Date(2016,3,03,17,30), 
	EndTime: new Date(2016,3,03,19,30), 
	Description: "Surgery Treatment", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 1, 
	OwnerId: 3, 
	Category: "extraction" 
}, { 
	Id: 14, 
	Subject: "Starc", 
	StartTime: new Date(2016,3,04,18,30), 
	EndTime: new Date(2016,3,04,21,30), 
	Description: "Complete Checkup after surgery", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 2, 
	OwnerId: 4, 
	Category: "observation" 
}, {
    Id: 15,
    Subject: "James",
    StartTime:  new Date(2016,3,03,19,00),
    EndTime:  new Date(2016,3,03,21,00),
	Description:"General Checkup",
    AllDay: false,
    Recurrence: false,
    RoomId: 1, 
	OwnerId: 5,
	Category: "therapy"
}, {
    Id: 16,
    Subject: "Jercy",
    StartTime:  new Date(2016,3,04,20,00),
    EndTime: new Date(2016,3,04,22,00),
	Description:"Health Checkup",
    AllDay: false,
    Recurrence: false,
	RoomId: 2, 
	OwnerId: 6,
	Category: "surgery"
}];
window.waitinglist = [
{ 
	Id: 1, 
	Subject: "Steven", 
	StartTime: new Date(2016,3,03,07,30), 
	EndTime: new Date(2016,3,03,09,30), 
	Description: "Master Checkup", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 1, 
	OwnerId: 1,
	dept: "GENERAL",
	Category: "consulting",
	Categorycolor: "#9A32CD"
}, { 
	Id: 2, 
	Subject: "Milan", 
	StartTime: new Date(2016,3,04,08,30), 
	EndTime: new Date(2016,3,04,10,30), 
	Description: "Master Checkup", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 2, 
	OwnerId: 2, 
	dept: "DENTAL",
	Category: "checkup",
	Categorycolor: "#458B74"
}, { 
	Id: 3, 
	Subject: "Laura", 
	StartTime: new Date(2016,3,04,09,30), 
	EndTime: new Date(2016,3,04,10,30), 
	Description: "Master Checkup", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 1, 
	OwnerId: 3, 
	dept: "GENERAL",
	Category: "extraction",
	Categorycolor: "#CD2626"	
}, { 
	Id: 4, 
	Subject: "Janet", 
	StartTime: new Date(2016,3,03,11,00), 
	EndTime: new Date(2016,3,03,12,30), 
	Description: "Master Checkup", 
	AllDay: false, 
	Recurrence: false, 
	RoomId: 2, 
	OwnerId: 4, 
	dept: "DENTAL",
	Category: "observation",
	Categorycolor: "#5F9EA0"
}];