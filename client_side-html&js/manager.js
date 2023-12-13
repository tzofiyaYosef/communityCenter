function Speaches(name, description, eventDate, numOfParticipance, costPerParticipance, eventCost, minAge, maxAge, gender, nameLecturer, id) {
    if (id != undefined)
        this.Id = id;
    this.Name = name;
    this.Description = description;
    this.EventDate = eventDate;
    this.NumOfParticipance = numOfParticipance;
    this.CostPerParticipance = costPerParticipance;
    this.EventCost = eventCost;
    this.MinAge = minAge;
    this.MaxAge = maxAge;
    this.Gender = gender;
    this.NameLecturer = nameLecturer;
}

function Trip(name, description, eventDate, numOfParticipance, costPerParticipance, eventCost, minAge, maxAge, gender, leavingTime, returnTime, location, id) {
    if (id != undefined)
        this.Id = id;
    this.Name = name;
    this.Description = description;
    this.EventDate = eventDate;
    this.NumOfParticipance = numOfParticipance;
    this.CostPerParticipance = costPerParticipance;
    this.EventCost = eventCost;
    this.MinAge = minAge;
    this.MaxAge = maxAge;
    this.Gender = gender;
    this.LeavingTime = leavingTime;
    this.ReturnTime = returnTime;
    this.Location = location;
}

function Manager(id, salary, password, firstName, lastName, phone, gmail) {
    this.Id = id;
    this.Salary = salary;
    this.Password = password;
    this.FirstName = firstName;
    this.LastName = lastName;
    this.Phone = phone;
    this.Gmail = gmail;
}

//תקינות קלט
function kelet(event) {
    let div = document.createElement('div')
    div.setAttribute('class', 'msgToUser')
    if (ev.id == '4' && ev.value != 9) {
        div.innerHTML = "שדה זה צריך להכיל מחרוזת באורך 9 תווים"
        event.appendChild(div)
    }

}

function init() {
    if (localStorage.getItem('gmail') !== null) {
        document.getElementById('user').innerHTML = 'שלום ' + localStorage.getItem('name')
        document.getElementById('user').href = 'prophilMamager.html'
        let temp = document.getElementById('join')
        temp.innerHTML = "התנתק"
        temp.href = '#'
        temp.onclick = function () { out() }
    }
    // else  document.getElementById('user').innerHTML =""
}

//פונק' התנתק מהמערכת
function out() {
    localStorage.clear()
    location.reload();
}

//הוצאת פרופיל של המנהל מהמערכת
function prophilManager() {
    init()
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/manager/GetManager/' + localStorage.getItem('gmail') + '/' + 1, true);
    //שליחת הבקשה
    req.send();
    // req.responseText קבלת התשובה למשתנה
    req.onload = function () {
        const manager = JSON.parse(req.responseText)
        //הדפסת התשובה על המסך
        document.getElementById('0').value = manager.FirstName
        document.getElementById('1').value = manager.LastName
        document.getElementById('2').value = manager.Phone
        document.getElementById('3').value = manager.Gmail
        document.getElementById('4').value = manager.Id
        document.getElementById('5').value = manager.Salary
        document.getElementById('6').value = manager.Password
    }
}

//הוספת מנהל
function addManager() {
    if (document.getElementById('6').value === document.getElementById('7').value) {
        let arr = [];
        for (let i = 0; i < 7; i++) {
            arr[i] = document.getElementById(i).value;
        }
        const manager = new Manager(arr[4], arr[5], arr[6], arr[0], arr[1], arr[2], arr[3]);
        //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
        // הכנסת הקובץ והאירוע לתוך משתנה
        let data = new FormData();
        //יוצר לי גי'סון ובתוכו שם את התמונה והאירוע
        data.append('manager', JSON.stringify(manager));

        //יצירת קריאה חדשה
        var xhr = new XMLHttpRequest();
        //פתיחת הקריאה - סוג וכתובת
        xhr.open('POST', 'https:/localhost:44397/API/manager/AddManager/', true);
        xhr.onload = function () {
            // do something to response
            if (xhr.response == "true") {
                localStorage.setItem("gmail", manager.Gmail)
                localStorage.setItem("id", manager.Id)
                localStorage.setItem("name", manager.FirstName + " " + manager.LastName)
                window.location.href = "manager.html"
            }
            else document.getElementById('msg').innerHTML = "מנהל זה כבר קיים במערכת"
        };
        //POSTצירוף המשתנים לתוך ה
        xhr.send(data);
    }
    else {
        document.getElementById('msg').innerHTML = "הסיסמאות אינן תואמות"
        document.getElementById('msg').style.backgroundColor = "rgba(0, 0, 0, 0.4)"
    }
}

//עדכון מנהל
function updatingManager() {
    let arr = [];
    for (let i = 0; i < 7; i++) {
        arr[i] = document.getElementById(i).value;
    }
    const manager = new Manager(arr[4], arr[5], arr[6], arr[0], arr[1], arr[2], arr[3]);
    manager.Id += ' ' + localStorage.getItem('id')
    manager.Gmail += ' ' + localStorage.getItem('gmail')
    //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
    // הכנסת הקובץ והאירוע לתוך משתנה
    let data = new FormData();
    //יוצר לי גי'סון ובתוכו שם את התמונה והאירוע
    data.append('manager', JSON.stringify(manager));

    //יצירת קריאה חדשה
    var xhr = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    xhr.open('POST', 'https:/localhost:44397/API/manager/updatingManager/', true);
    xhr.onload = function () {
        // do something to response
        if (xhr.response === 'true') {
            out()
            localStorage.setItem('name', document.getElementById('0').value + " " + document.getElementById('1').value)
            localStorage.setItem('gmail', document.getElementById('3').value)
            localStorage.setItem('id', document.getElementById('4').value)
            alert("הפרטים עודכנו בהצלחה!!")
            init()
        }
        else {
            location.reload()
            window.alert("אין אפשרות לשנות ת.ז.")
        }
    };
    //POSTצירוף המשתנים לתוך ה
    xhr.send(data);
}

//מחיקת מנהל
function deletManager() {
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/manager/deletManager/' + localStorage.getItem('gmail') + '/' + 1, true);
    //שליחת הבקשה
    req.send();
    // req.responseText קבלת התשובה למשתנה
    req.onload = function () {
        //הדפסת התשובה על המסך
        out()
        init()
        window.location.href = 'manager.html'
    }
}

//הוספת טיול או הרצאה
function addNewEvent() {
    let arr = [];
    let gender;
    switch (document.getElementById(8).value) {
        case "1": gender = "נשים ונערות"; break;
        case "2": gender = "גברים"; break;
        case "3": gender = "ילדות"; break;
        case "4": gender = "ילדים"; break;
        case "5": gender = "משפחות"; break;
        default: gender = "זוגות";
    }
    let length;
    if (document.getElementById('10') == null)
        length = 9;
    else length = 11;
    for (let i = 0; i <= length; i++) {
        arr[i] = document.getElementById(i).value;
    }
    //שולף את הקובץ
    const selectedFile = document.getElementById('myFile').files[0];
    if (length == 11) {
        const trip = new Trip(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], gender, arr[9], arr[10], arr[11]);
        //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
        console.log(selectedFile);
        // הכנסת הקובץ והאירוע לתוך משתנה
        let data = new FormData();
        //יוצר לי גי'סון ובתוכו שם את התמונה והאירוע
        data.append('trip', JSON.stringify(trip));
        data.append('file', selectedFile)

        //יצירת קריאה חדשה
        var xhr = new XMLHttpRequest();
        //פתיחת הקריאה - סוג וכתובת
        xhr.open('POST', 'https:/localhost:44397/API/mainEvents/AddTrip/', true);
        xhr.onload = function () {
            // do something to response
            if (xhr.responseText == 'true')
                window.location.href = "managerAllEvaents.html"
        };
        //POSTצירוף המשתנים לתוך ה
        xhr.send(data);
    }
    else if (length == 9) {
        const speaches = new Speaches(arr[0], arr[1], arr[2] + " " + document.getElementById("appt").value, arr[3], arr[4], arr[5], arr[6], arr[7], gender, arr[9]);
        //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
        console.log(selectedFile);
        // הכנסת הקובץ והאירוע לתוך משתנה
        let data = new FormData();
        //יוצר לי גי'סון ובתוכו שם את התמונה והאירוע
        data.append('speaches', JSON.stringify(speaches));
        data.append('file', selectedFile)

        //יצירת קריאה חדשה
        var xhr = new XMLHttpRequest();
        //פתיחת הקריאה - סוג וכתובת
        xhr.open('POST', 'https:/localhost:44397/API/mainEvents/AddSpeach/', true);
        xhr.onload = function () {
            // do something to response
            if (xhr.response === 'true')
                window.location.href = "managerAllEvaents.html"
        };
        //POSTצירוף המשתנים לתוך ה
        xhr.send(data);
    }
}

function cards(id) {
    init()
    if (id != undefined) {
        localStorage.setItem ('eventId', id) 
        window.location.href = "detailsEvent.html"
    }
    else {
        let req = new XMLHttpRequest()
        req.open('get', 'https://localhost:44397/API/mainEvents/EventProphil/' + localStorage.getItem('eventId'), true)
        req.send()
        req.onload = function () {
            const MainEvent = JSON.parse(req.response)
            let details = document.createElement('div')
            details.setAttribute('class', 'details')
            let image = document.createElement('img')
            image.setAttribute('class', 'imgD')
            image.src = MainEvent.ImagePath
            let title = document.createElement('div')
            title.setAttribute('class', 'titleEvent')
            title.innerHTML = MainEvent.Name
            details.appendChild(title)
            let allBuy = document.createElement('div')
            allBuy.setAttribute('class', 'allBuy')
            let req2 = new XMLHttpRequest()
            req2.open('get', 'https://localhost:44397/API/mainEvents/RemainingTickets/' + localStorage.getItem('eventId') + '/' + 0, true)
            req2.send()
            req2.onload = function () {
                allBuy.innerHTML ='כרטיסים שנותרו: ' + req2.response + ' מתוך ' +MainEvent.NumOfParticipance 
                details.appendChild(allBuy)
                document.getElementById('containerPlace').appendChild(image)
                document.getElementById('containerPlace').appendChild(details)
            }
        }
    }
}

//הצגת כל האירועים
function allEvent(str) {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/' + `${str}`, true)
    req.send()
    req.onload = function () {
        let arr = JSON.parse(req.responseText)
        for (let j = 0; j < arr.length; j++)
        for (let k = 0; k < arr.length - j - 1; k++) {
            if (new Date(arr[k].EventDate) > new Date(arr[k + 1].EventDate)) {
                let temp = arr[k]
                arr[k] = arr[k + 1]
                arr[k + 1] = temp
            }
        }
        for (let i = 0; i < arr.length; i++) {
            let card2 = document.createElement('div')
            card2.setAttribute('class', 'card2')
            let new_a = document.createElement('a')
            new_a.onclick = function () { returnIdEvent(arr[i]) }
            let card = document.createElement('div')
            card.setAttribute('class', 'card')
            let pic = document.createElement('div')
            pic.setAttribute('class', 'pic')
            let date = document.createElement('div')
            let dt = new Date(arr[i].EventDate)
            date.innerHTML = `${appendLeadingZeros(dt.getDate())}` + '/' + `${appendLeadingZeros(dt.getMonth()+1)}`
            date.setAttribute('class', 'date')
            pic.appendChild(date)
            let image = document.createElement('img')
            image.setAttribute('class', 'imgEvevt')
            image.src = arr[i].ImagePath
            pic.appendChild(image)
            new_a.appendChild(pic)
            let title = document.createElement('h1')
            title.innerHTML = arr[i].Name
            new_a.appendChild(title)
            let dateEvent = document.createElement('h2')
            dateEvent.innerHTML = `${appendLeadingZeros(dt.getDate())}` + '/' + `${appendLeadingZeros(dt.getMonth()+1)}` + '/' + `${appendLeadingZeros(dt.getFullYear())}`
            new_a.appendChild(dateEvent)
            let price = document.createElement('h3')
            price.setAttribute('class', 'price')
            if (arr[i].LeavingTime == undefined)
                price.innerHTML = `${arr[i].EventDate.slice(11, 16)}`;
            else price.innerHTML = arr[i].LeavingTime
            new_a.appendChild(price)
            let description = document.createElement('p')
            description.setAttribute('class', 'description')
            description.innerHTML = `${arr[i].Description}`;
            new_a.appendChild(description)
            //add a p - few on the event
            let btn = document.createElement('button')
            btn.innerHTML = "מעקב אחרי כרטיסים"
            btn.onclick = function () { cards(arr[i].Id) }
            card.appendChild(new_a)
            card.appendChild(btn)
            card2.appendChild(card)
            document.getElementById('allEvent').appendChild(card2)
        }
    }
    if (str === "AllTrip/") {
        allEvent("AllSpeache/")
        init()
    }
}

//הצגת התאריך בפורמט ברור
function appendLeadingZeros(int) {
    if (int < 10) {
        return '0' + int;
    }
    return int;
}

//החזרת פרופיל אירוע
function returnIdEvent(vec) {
    localStorage.setItem('eventId', vec.Id)
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/typeEvent/' + localStorage.getItem('eventId'), true)
    req.send()
    req.onload = function () {
        if (req.responseText === '"trip"')
            window.location.href = "tripProphil.html"
        else window.location.href = "speacheProphil.html"
    }
}

//פרופיל של טיול ספציפי
function tripProphil() {
    init()
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/tripProphil/' + localStorage.getItem('eventId'), true)
    req.send()
    req.onload = function () {
        const trip = JSON.parse(req.responseText)
        //הדפסת התשובה על המסך
        document.getElementById('0').value = trip.Id
        document.getElementById('1').value = trip.Name
        document.getElementById('2').value = trip.EventDate.slice(0, 10)
        document.getElementById('3').value = trip.NumOfParticipance
        document.getElementById('4').value = trip.CostPerParticipance
        document.getElementById('5').value = trip.EventCost
        document.getElementById('6').value = trip.MinAge
        document.getElementById('7').value = trip.MaxAge
        document.getElementById('8').value = trip.Gender
        document.getElementById('9').value = trip.LeavingTime
        document.getElementById('10').value = trip.ReturnTime
        document.getElementById('11').value = trip.Description
        document.getElementById('12').value = trip.Location
        document.getElementById('tripImg').src = trip.ImagePath
        localStorage.setItem('imgPath', trip.ImagePath)
    }
}

//פרופיל של הרצאה 
function speacheProphil() {
    init()
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/SpeacheProphil/' + localStorage.getItem('eventId'), true)
    req.send()
    req.onload = function () {
        const speache = JSON.parse(req.responseText)
        //הדפסת התשובה על המסך
        document.getElementById('0').value = speache.Id
        document.getElementById('1').value = speache.Name
        document.getElementById('2').value = speache.EventDate.slice(0, 10)
        document.getElementById('3').value = speache.NumOfParticipance
        document.getElementById('4').value = speache.CostPerParticipance
        document.getElementById('5').value = speache.EventCost
        document.getElementById('6').value = speache.MinAge
        document.getElementById('7').value = speache.MaxAge
        document.getElementById('8').value = speache.Gender
        document.getElementById('9').value = speache.NameLecturer
        document.getElementById('10').value = speache.Description
        document.getElementById('11').value = speache.EventDate.slice(11, 16)
        document.getElementById('speacheImg').src = speache.ImagePath
        localStorage.setItem('imgPath', speache.ImagePath)
    }
}

//החלפת תמונה של אירוע
function raise(event, str, bit) {
    if (bit === 1) {
        let image = document.createElement('img')
        image.setAttribute("class", str)
        let from = document.getElementsByName("form")
        from.appendChild(image)
    }
    var selectedFile = event.target.files[0];
    var reader = new FileReader();

    var imgtag = document.getElementById(str);
    imgtag.title = selectedFile.name;

    reader.onload = function (event) {
        imgtag.src = event.target.result;
    };

    reader.readAsDataURL(selectedFile);
}

//עדכון פרטי טיול
function updatingTrip() {
    let arr = [];
    for (let i = 0; i < 13; i++) {
        arr[i] = document.getElementById(i).value;
    }
    const trip = new Trip(arr[1], arr[11], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], arr[8], arr[9], arr[10], arr[12], localStorage.getItem('eventId'));
    trip.ImagePath = localStorage.getItem("imgPath")
    //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
    // הכנסת הקובץ והאירוע לתוך משתנה
    let data = new FormData();
    //יוצר לי גי'סון ובתוכו שם את התמונה והאירוע
    data.append('trip', JSON.stringify(trip));
    const selectedFile = document.getElementById('myFile').files[0];
    // if (selectedFile == undefined)
    //     selectedFile = "null"
    data.append('file', selectedFile)
    //יצירת קריאה חדשה
    var xhr = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    xhr.open('POST', 'https:/localhost:44397/API/mainEvents/updatingTrip/', true);
    xhr.onload = function () {
        // do something to response
        if (xhr.response === 'true') {
            window.alert("פרטי טיול עודכנו בהצלחה!!")
            tripProphil()
        }
    };
    //POSTצירוף המשתנים לתוך ה
    xhr.send(data);
}

//עדכון פרטי הרצאה
function updatingSpeache() {
    let arr = [];
    for (let i = 0; i < 11; i++) {
        arr[i] = document.getElementById(i).value;
    }
    arr[2] += " " + document.getElementById('11').value
    const speache = new Speaches(arr[1], arr[10], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], arr[8], arr[9], localStorage.getItem('eventId'));
    speache.ImagePath = localStorage.getItem("imgPath")
    //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
    // הכנסת הקובץ והאירוע לתוך משתנה
    let data = new FormData();
    //יוצר לי גי'סון ובתוכו שם את התמונה והאירוע
    data.append('speache', JSON.stringify(speache));
    const selectedFile = document.getElementById('myFile').files[0];
    // if (selectedFile == undefined)
    //     selectedFile = "null"
    data.append('file', selectedFile)
    //יצירת קריאה חדשה
    var xhr = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    xhr.open('POST', 'https:/localhost:44397/API/mainEvents/updatingSpeache/', true);
    xhr.onload = function () {
        // do something to response
        if (xhr.response === 'true') {
            window.alert("פרטי הרצאה עודכנו בהצלחה!!")
            speacheProphil()
        }
    };
    //POSTצירוף המשתנים לתוך ה
    xhr.send(data);
}

//מחיקת אירוע
function deletEvent(str) {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/' + `${str}` + localStorage.getItem('eventId'), true)
    req.send()
    req.onload = function () {
        location.reload()
        window.alert('הוסר בהצלחה!!')
        window.location.href = "managerAllEvaents.html"
    }
}

function start() {
    let img = document.createElement('img')
    img.src = localStorage.getItem('src')
    document.getElementById('in').appendChild(img)


}

//הזזת תמונת הרקע
function showSlides() {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("dot");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
    setTimeout(showSlides, 6000); // Change image every 6 seconds
}

function deletAnswer(id) {
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/contacs/deletQuestion/' + id, true);
    //שליחת הבקשה
    req.send();
    req.onload = function () {
        alert("הפניה הוסרה בהצלחה!")
        location.reload()
    }
}

//כל הפניות עם התשובות
function otherQuestion() {
    init()
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/contacs/GetAllQuetions', true);
    //שליחת הבקשה
    req.send();
    req.onload = function () {
        console.log(req.responseText);
        let allRequest = JSON.parse(req.response);
        for (let i = 0; i < allRequest.length; i++) {
            let new_btn = document.createElement('button')
            new_btn.value = allRequest[i].contactId
            new_btn.innerHTML = allRequest[i].Question
            new_btn.className = "accordion"
            let new_div = document.createElement('div')
            new_div.className = "panel"
            document.getElementById('contact').appendChild(new_btn)
            document.getElementById('contact').appendChild(new_div)
            if (allRequest[i].Answer !== null) {
                let new_p = document.createElement('p')
                new_p.innerHTML = allRequest[i].Answer
                new_div.appendChild(new_p)
                let new_btn1 = document.createElement('input')
                new_btn1.type = "button"
                new_btn1.value = "מחיקת פניה"
                new_btn1.onclick = function () { deletAnswer(allRequest[i].contactId) }
                new_div.appendChild(input)
                new_div.appendChild(new_btn1)
            }
        }
        let question = document.getElementsByClassName("accordion");
        for (let i = 0; i < question.length; i++) {
            question[i].addEventListener("click", function () {
                /* Toggle between adding and removing the "active" class,
                to highlight the button that controls the panel */
                this.classList.toggle("active");

                /* Toggle between hiding and showing the active panel */
                var panel = this.nextElementSibling;
                if (panel.style.display === "block") {
                    panel.style.display = "none";
                } else {
                    panel.style.display = "block";
                }
            });
        }
    }
}

//הצגת כל הפניות והאפשרות להכניס להם תשובה
function allQuestion() {
    init()
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/contacs/GetEmptyQuetions/', true);
    //שליחת הבקשה
    req.send();
    req.onload = function () {
        console.log(req.responseText);
        let allRequest = JSON.parse(req.response);
        for (let i = 0; i < allRequest.length; i++) {
            let new_btn = document.createElement('button')
            new_btn.value = allRequest[i].contactId
            new_btn.innerHTML = allRequest[i].Question
            new_btn.className = "accordion"
            let new_div = document.createElement('div')
            new_div.className = "panel"
            document.getElementById('contact').appendChild(new_btn)
            document.getElementById('contact').appendChild(new_div)
            if (allRequest[i].Answer !== null) {
                let new_p = document.createElement('p')
                new_p.innerHTML = allRequest[i].Answer
                new_div.appendChild(new_p)
            }
            else {
                let input = document.createElement('input')
                input.type = "text"
                input.className = "answer"
                let new_btn1 = document.createElement('input')
                new_btn1.type = "button"
                new_btn1.value = "שליחה"
                new_btn1.onclick = function () { insertAnswer(new_btn.value, input.value) }
                new_div.appendChild(input)
                new_div.appendChild(new_btn1)
            }
        }
        let question = document.getElementsByClassName("accordion");
        for (let i = 0; i < question.length; i++) {
            question[i].addEventListener("click", function () {
                /* Toggle between adding and removing the "active" class,
                to highlight the button that controls the panel */
                this.classList.toggle("active");

                /* Toggle between hiding and showing the active panel */
                var panel = this.nextElementSibling;
                if (panel.style.display === "block") {
                    panel.style.display = "none";
                } else {
                    panel.style.display = "block";
                }
            });
        }
        if (allRequest.length == 0) {
            let p = document.createElement('p')
            p.className = 'p'
            p.innerHTML = "אין פניות חדשות"
            document.getElementById('contact').appendChild(p)
        }
    }
}

//הכנסת התשובה לסי שארפ וגם רענון הדף
function insertAnswer(contactId, answer) {
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/contacs/AddAnswerToQuestion/' + contactId + '/' + answer, true);
    //שליחת הבקשה
    req.send();
    req.onload = function () {
        if (req.responseText)
            window.location.href = "manager.html"
    }
}



// const arr = []
// arr.add("https://www.netivim.org.il/content/images/logo.png")
// function addBackgroundImage() {
//     arr.add(document.getElementById('myFile').files[0])

//     showSlides()
// }
