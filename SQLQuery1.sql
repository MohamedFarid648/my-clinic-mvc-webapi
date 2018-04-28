Create table Clinic(Id int Primary Key,Name varchar(20),Address varchar(20))

Create table Doctor(Id varchar(128) Primary Key,ClinicID int,

    FOREIGN KEY (ClinicID) REFERENCES Clinic(Id)

)

Create table Patient(Id varchar(128) Primary Key,ClinicID int

constraint c foreign key(ClinicID) references Clinic(Id)
)

Create table Nurse(Id varchar(128) Primary Key,ClinicID int

constraint c1 foreign key(ClinicID) references Clinic(Id)
)


Create table Request(Id int Primary Key,MyDateTime datetime,NurseId varchar(128),DoctorId varchar(128),PatientId varchar(128)

constraint c2 foreign key(NurseId) references Nurse(Id),
constraint c3 foreign key(DoctorId) references Doctor(Id),
constraint c4 foreign key(PatientId) references Patient(Id)

)

Create table Medicine(Id int Primary Key,Quty int ,NurseId varchar(128),DoctorId varchar(128),PatientId varchar(128)

constraint c5 foreign key(DoctorId) references Doctor(Id),
constraint c6 foreign key(PatientId) references Patient(Id)

)
