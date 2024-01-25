# HospitalManagementSystem
Small hospital management system project which can take patient request and based on disease will forward it to doctor . the project should implement rest with radish cache and simple ui for  user login .

to run this project on visual studio 2019 : 
make sure you have Newtonsoft.Json and distributed cache library . 
you can fill the patient registration from on post request : 
https://localhost:44374/patient/submit

{"patientName": "Aparna Kulkarni",
 "disease": "orthopedic"
}
after that you will get the success popup on postman and get id {patient:afca6e77-f97e-45b7-9a09-8b8eeab4d4b7
}
then 
use httpget request to get the doctor details : 
https://localhost:44374/doctor/assign/patient:afca6e77-f97e-45b7-9a09-8b8eeab4d4b7
## This is just a normal experiment : 
I use chat gpt prompt to generate the code . 
run successfully . 
It works fine. 
**Thanks ChatGPT*** 
