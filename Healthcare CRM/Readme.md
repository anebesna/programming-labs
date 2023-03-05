This is an Informational System for Healthcare organisations.
The task is formulated as follows: Create an information system for medical organizations in the city.
Each hospital in the city consists of one or several buildings, each of which houses one or several departments specializing in the treatment of a certain group of diseases. Each department has a certain number of wards with a specific number of beds. Clinics may be administratively attached to hospitals or may not be.

Both hospitals and clinics are served by medical staff (surgeons, therapists, neurologists, ophthalmologists, dentists, radiologists, gynecologists, and others) and service personnel (nurses, orderlies, janitors, and others). Each category of medical personnel has characteristics unique to their field of expertise and participates in different ways: surgeons, dentists, and gynecologists may perform surgeries and have characteristics such as the number of surgeries performed and the number of surgeries resulting in fatalities; radiologists and dentists have a coefficient for hazardous working conditions, while radiologists and neurologists have longer vacations.

Doctors of any specialty may have a candidate or doctor of medical sciences degree. A doctor of medical sciences degree gives the right to the title of professor, and a candidate of medical sciences degree gives the right to the title of associate professor. Joint employment is allowed, so each doctor can work in a hospital, clinic, or both. Doctors with the title of associate professor or professor will provide consulting assistance in several hospitals or clinics.

Outpatients are treated in one of the clinics and may be hospitalized based on their referral either in the hospital to which the clinic is attached or in any other hospital if the specialization of the hospital to which the clinic is assigned does not allow for the necessary treatment. Both hospitals and clinics keep personalized patient records, including the full history of their illnesses, all prescriptions, surgeries, etc. In the hospital, the patient has one treating doctor at any given moment, while in the clinic, they may have several doctors involved in their care.

---

The functionality of the app allows to add new healthcare organisation or delete one, hire or fire medical employee or staff, admit or discharge a patient, add new departments or delete them. 
In the window "Connections" you can manage all the connections e.g. assign a doctor to a patient, bind a doctor to specific department or hospital, assign medical interventions and much more.
In the window CurrentState doctors can add data about recent analysis of the patient's state.
In the window HealthCard doctors can find the medical history of the particular patient as well as in MedInterventions about medical interventions of this patient.
Also there is an audit log to track who used the CRM at the partiular time. And obviously there is a log in window to authenticate the user.

Windows with Query... name are additional tasks where you can perform next actions:
<ol>
  <li>Queries: 
    <ul><li>Obtain a list and total number of doctors of a specific specialty for a particular medical institution, hospital, or clinic, or for all medical facilities in the city.</li>
    <li>Obtain a list and total number of supporting personnel of a specific specialty for a particular medical institution, hospital, or clinic, or for all medical facilities in the city.</li>
    <li>Obtain a list and total number of doctors of a specific specialty who have performed at least a certain number of operations for a particular medical institution, hospital, or clinic, or for all medical facilities in the city.</li>
    <li>Obtain a list and total number of doctors of a specific specialty whose work experience is at least a certain duration for a particular medical institution, hospital, or clinic, or for all medical facilities in the city.</li>
    <li>Obtain a list and total number of doctors of a specific specialty with a candidate or doctorate degree in medical science, or with the title of associate professor or professor, for a particular medical institution, hospital, or clinic, or for all medical facilities in the city.</li></ul></li>
  <li>Queries2:<ul>
  <li>Get a list of patients from the specified hospital, department, or specific ward of the department, with the date of admission, condition, temperature, and attending physician specified.</li>
  <li>Get a list of patients who underwent inpatient treatment at the specified hospital or with a specific doctor for a certain period of time.</li>
  <li>Get a list of patients being observed by a doctor of the specified profile at a specific clinic.</li></ul></li>
<li>Query_9:<ul>
  <li>Get the total number of wards, beds of the specified hospital overall and per each department, as well as the number of available beds in each department and the number of fully vacant wards.</li></ul></li>
  </ol>
