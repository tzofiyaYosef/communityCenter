function Customer(userName, password, firstName, lastName, phone, gmail) {
    this.FirstName = firstName;
    this.LastName = lastName;
    this.Phone = phone;
    this.Gmail = gmail;
    this.UserName = userName;
    this.Password = password;
}

function MainEventToCustomer(idEvent, idCustomer, status, id) {
    if (id != undefined)
        this.Id = id
    this.IdEvent = idEvent;
    this.IdCustomer = idCustomer;
    this.Status = status;
}

//שליחת הפנייה
function sendRequest() {
    //שליפת השם לשליחה
    let gmail = localStorage.getItem('gmail')
    let q = document.querySelector('#subject').value;
    //יצירת קריאה חדשה
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/contacs/AddQuestion/' + gmail + '/' + q, true);
    //שליחת הבקשה
    req.send();
    // req.responseText קבלת התשובה למשתנה
    req.onload = function () {
        //הדפסת התשובה על המסך
        window.location.href = "ok.html"
    }
}

let count = 0;

//פונקציה שמחברת לקוחות ומנהלים
function login() {
    //שליפת השם לשליחה
    let gmail = document.querySelector('#gmail').value;
    let name = document.querySelector('#username').value;
    let pass = document.querySelector('#password').value;
    // הכנסת השם לסטורג' למשך כל ריצת הדף
    // localStorage.setItem("name", ""+name)
    //יצירת קריאה חדשה
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/customer/GetLogin/' + gmail + '/' + name + '/' + pass, true);
    //שליחת הבקשה
    req.send();
    req.onload = function () {
        if (req.responseText == "0")
            document.querySelector(".btn").innerHTML = "משתמש זה לא קיים במערכת, להרשמה לחץ כאן"
        if (req.responseText === "3") {
            count++;
            document.querySelector(".forgotPass").innerHTML = "?שכחת סיסמה"
            if (count > 3) {
                document.querySelector(".forgotPass").innerHTML = " "
                document.querySelector(".btn").innerHTML = "משתמש זה לא קיים במערכת, להרשמה לחץ כאן"
            }
        }
        else {
            localStorage.setItem("gmail", gmail);

            if (req.responseText === "1") {
                localStorage.setItem("username", name)
                window.location.href = "user.html";
                console.log(req.responseText);
            }
            else if (req.responseText === "2") {
                let req2 = new XMLHttpRequest();
                //פתיחת הקריאה - סוג וכתובת
                req2.open('get', 'https:/localhost:44397/API/manager/GetManager/' + gmail + '/' + 1, true);
                //שליחת הבקשה
                req2.send();
                req2.onload = function () {
                    const manager = JSON.parse(req2.response)
                    localStorage.setItem("id", manager.Id);
                    localStorage.setItem("name", name)
                    window.location.href = "manager.html"
                }
            }
        }
    }
}

//בדיקה האם המשתמש חדש ונרשם עכשיו
function click() {
    if (init() === true)
        window.location.href = "ContactUs.html"
}

//בכל מעבר בין עמוד הפונ' בודקת האם מישהו מחובר, במידה ולא היא תראה לו את אופציית התחבר
function init() {
    // let name = localStorage.getItem('username')
    if (localStorage.getItem('isNewUser') === "true") {
        document.getElementById('username').value = localStorage.getItem('username')
        document.getElementById('gmail').value = localStorage.getItem('gmail')
        document.getElementById('password').value = localStorage.getItem('pass')
        localStorage.setItem('isNewUser', "false")
    }
    // else if (name !== null)
    //     document.getElementById('username').value = name
    if (localStorage.getItem('gmail') !== null) {
        // prudctInCart()
        document.querySelector("#join").innerHTML = "הפניות שלי"
        document.querySelector("#join").href = "allRequest.html"
        document.querySelector("#change").href = "ContactUs.html"
        let x = document.querySelector("#join")
        // x.onclick = function () { allRequest() }
        // ************************
        let new_li = document.createElement('li')
        let new_a = document.createElement('a')
        new_a.innerHTML = 'התנתק'
        new_a.href = 'user.html'
        new_a.onclick = function () { out() }
        new_li.appendChild(new_a)
        document.querySelector('#add').appendChild(new_li)
        // let newli = document.createElement('li')
        document.getElementById('user').innerHTML = 'שלום ' + localStorage.getItem('username')
        document.getElementById('user').href = 'prophil.html'
        let req = new XMLHttpRequest()
        req.open('get', 'https:/localhost:44397/API/customer/GetAllEventInCart/' + localStorage.getItem('gmail') + '/' + 0, true);
        req.send()
        // צריך לבדוק למה הוא לא נכנס לתוך הפונק'
        req.onload = function () {
            let numProduct = document.createElement('div')
            numProduct.setAttribute('class', 'numProduct')
            numProduct.innerHTML = JSON.parse(req.response).length
            document.getElementById('cart').appendChild(numProduct)
        }
    }
}

//פונק' התנתק מהמערכת
function out() {
    localStorage.clear()
}

//החלפת סיסמא
function examination() {
    let gmail = document.querySelector('#gmail').value;
    let userName = document.querySelector('#userName').value;
    let pass1 = document.querySelector('#password1').value;
    let pass2 = document.querySelector('#password2').value;
    if (pass1 !== pass2) {
        document.getElementById('ex').className += "bottom-container"
        document.querySelector('.btn').innerHTML = "נא הכנס סיסמאות תואמות"
    }
    else {
        let req = new XMLHttpRequest();
        //פתיחת הקריאה - סוג וכתובת
        req.open('get', 'https:/localhost:44397/API/customer/examinationGmail/' + gmail + '/' + userName + '/' + pass1 + '/' + pass2, true);
        //שליחת הבקשה
        req.send();
        req.onload = function () {
            if (req.responseText === "0") {
                document.querySelector("#btn").value = "משתמש זה לא קיים במערכת, להרשמה לחץ כאן"
            }
            else window.location.href = "index.html"
            // else if (document.querySelector("#password1").value === document.querySelector("#password2").value) {
            //     // let newreq = new XMLHttpRequest();
            //     // newreq.open('get', 'https:/localhost:44397/API/customer/changeGmail/' + gmail + '/' + document.querySelector("#password1").value, true);
            //     // newreq.send();
            //     // if (newreq.responseText)
            //    
            // }

        }
    }
}

//החזרת פרטי משתמש
function user() {
    init()
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/customer/GetCustomer/' + localStorage.getItem('gmail') + '/' + 1, true);
    //שליחת הבקשה
    req.send();
    // req.responseText קבלת התשובה למשתנה
    req.onload = function () {
        const customer = JSON.parse(req.responseText)
        //הדפסת התשובה על המסך
        document.getElementById('0').value = customer.FirstName
        document.getElementById('1').value = customer.LastName
        document.getElementById('2').value = customer.Phone
        document.getElementById('3').value = customer.Gmail
        document.getElementById('4').value = customer.UserName
        document.getElementById('5').value = customer.Password
    }
}

//עדכון פרטי משתמש
function updatingUser() {
    let arr = [];
    for (let i = 0; i < 6; i++) {
        arr[i] = document.getElementById(i).value;
    }
    const customer = new Customer(arr[4], arr[5], arr[0], arr[1], arr[2], arr[3]);
    customer.Gmail += ' ' + localStorage.getItem('gmail')
    //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
    // הכנסת הקובץ והאירוע לתוך משתנה
    let data = new FormData();
    //יוצר לי גי'סון ובתוכו שם את התמונה והאירוע
    data.append('customer', JSON.stringify(customer));

    //יצירת קריאה חדשה
    var xhr = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    xhr.open('POST', 'https:/localhost:44397/API/customer/updatingCustomer/', true);
    xhr.onload = function () {
        // do something to response
        if (xhr.response === 'true') {
            out()
            localStorage.setItem('username', document.getElementById('4').value)
            localStorage.setItem('gmail', document.getElementById('3').value)
            user()
            location.reload()
            window.alert('פרטי המשתמש עודכנו בהצלחה!')
        }
        else {
            location.reload()
            alert("אין אפשרות לשנות את המייל")
        }
    };
    //POSTצירוף המשתנים לתוך ה
    xhr.send(data);
}

//מחיקת משתמש קיים
function deletUser() {
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/customer/deletCustomer/' + localStorage.getItem('gmail') + '/' + 1, true);
    //שליחת הבקשה
    req.send();
    // req.responseText קבלת התשובה למשתנה
    req.onload = function () {
        //הדפסת התשובה על המסך
        out()
        init()
        window.location.href = 'user.html'
    }
}

//לבדוק מה זה???
// function getQuestion() {
//     //יצירת קריאה חדשה
//     let str = "start";
//     console.log(str);
//     let allQuestion = new XMLHttpRequest();
//     //פתיחת הקריאה - סוג וכתובת
//     allQuestion.open('get', 'https:/localhost:44397/API/customer/GetQuestionWithoutAnswers', true);
//     //שליחת הבקשה
//     allQuestion.send();
//     // req.responseText קבלת התשובה למשתנה
//     allQuestion.onload = function () {
//         //הדפסת התשובה על המסך
//         window.location.href = "ok.html"
//     }
// }

//כל הפניות של כל המשתמשים
function otherQuestion() {
    init()
    let gmail = localStorage.getItem("gmail")
    //יצירת קריאה חדשה
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/contacs/GetAllQuetions', true);
    //שליחת הבקשה
    req.send();
    req.onload = function () {
        let allRequest = JSON.parse(req.response);
        if (allRequest.length !== 0) {
            console.log(allRequest);
            let table = document.createElement('table')
            table.setAttribute('class', 'table table-primary table-bordered table-hover')

            //שורת כותרת
            let r = document.createElement('tr');
            //עמודות כותרת
            let d1 = document.createElement('th');
            d1.innerHTML = "תשובה"
            r.appendChild(d1)
            d1 = document.createElement('th');
            d1.innerHTML = "שאלה"
            r.appendChild(d1)
            // הוספת שורת כותרת
            table.appendChild(r)
            //שורות תוכן
            for (let i = 0; i < allRequest.length; i++) {
                r = document.createElement('tr');
                //Answer
                d1 = document.createElement('td');
                if (allRequest[i].Answer != null)
                    d1.innerHTML = allRequest[i].Answer
                else d1.innerHTML = "פנייתכם בטיפול ותענה בהקדם, תודה על סבלנותכם."
                r.appendChild(d1)
                //Question

                d1 = document.createElement('td');
                d1.innerHTML = allRequest[i].Question
                r.appendChild(d1)

                // הוספת שורות תוכן
                table.appendChild(r)

            }

            document.getElementById('contact').appendChild(table)
        }
    }
}

//פרטי אירוע
function cards(id) {
    init()
    if (id != undefined) {
        localStorage.setItem('eventId', id)
        window.location.href = "detailsEventUser.html"
    }
    else {
        let req = new XMLHttpRequest()
        req.open('get', 'https://localhost:44397/API/mainEvents/EventProphil/' + localStorage.getItem('eventId'), true)
        req.send()
        req.onload = function () {
            const MainEvent = JSON.parse(req.response)
            let details = document.createElement('div')
            details.setAttribute('class', 'detalis ddd')
            let image = document.createElement('img')
            image.setAttribute('class', 'imgD')
            image.src = MainEvent.ImagePath
            let title = document.createElement('div')
            title.setAttribute('class', 'titleEvent')
            title.innerHTML = MainEvent.Name
            details.appendChild(title)
            let date = document.createElement('div')
            date.setAttribute('class', 'allBuy')
            let dt = new Date(MainEvent.EventDate)
            date.innerHTML = "תאריך האירוע: " + `${appendLeadingZeros(dt.getDate())}` + '/' + `${appendLeadingZeros(dt.getMonth())}` + '/' + `${appendLeadingZeros(dt.getFullYear())}`
            details.appendChild(date)
            let time = document.createElement('div')
            time.setAttribute('class', 'allBuy')
            time.innerHTML = "שעת התחלה: " + MainEvent.EventDate.slice(11, 16)
            details.appendChild(time)
            let price = document.createElement('div')
            price.setAttribute('class', 'allBuy')
            price.innerHTML = "מחיר: " + MainEvent.CostPerParticipance + ' ש"ח'
            details.appendChild(price)
            let btn = document.createElement('button')
            btn.setAttribute('class', 'btn-add')
            btn.innerHTML = "הוסף לעגלה>>>"
            btn.onclick = function () { buyCard(MainEvent.Id) }
            details.appendChild(btn)
            document.getElementById('containerDiff').appendChild(image)
            document.getElementById('containerDiff').appendChild(details)
        }
    }
}

//פונק' שמראה  לכל משתמש את הפניות שלו והתשובות עליהם
function allRequest() {
    init()
    let gmail = localStorage.getItem("gmail")
    //יצירת קריאה חדשה
    let req = new XMLHttpRequest();
    //פתיחת הקריאה - סוג וכתובת
    req.open('get', 'https:/localhost:44397/API/contacs/GetUserQuetions/' + gmail + '/' + 1, true);
    //שליחת הבקשה
    req.send();
    req.onload = function () {
        let allRequest = JSON.parse(req.response);
        if (allRequest.length !== 0) {
            console.log(allRequest);
            let table = document.createElement('table')
            table.setAttribute('class', 'table table-primary table-bordered table-hover')

            //שורת כותרת
            let r = document.createElement('tr');
            //עמודות כותרת
            let d1 = document.createElement('th');
            d1.innerHTML = "תשובה"
            r.appendChild(d1)
            d1 = document.createElement('th');
            d1.innerHTML = "שאלה"
            r.appendChild(d1)
            // הוספת שורת כותרת
            table.appendChild(r)
            //שורות תוכן
            for (let i = 0; i < allRequest.length; i++) {
                r = document.createElement('tr');
                //Answer
                d1 = document.createElement('td');
                if (allRequest[i].Answer != null)
                    d1.innerHTML = allRequest[i].Answer
                else d1.innerHTML = "פנייתך בטיפול ותענה בהקדם, תודה על סבלנותך."
                r.appendChild(d1)
                //Question

                d1 = document.createElement('td');
                d1.innerHTML = allRequest[i].Question
                r.appendChild(d1)

                // הוספת שורות תוכן
                table.appendChild(r)

            }

            document.getElementById('contact').appendChild(table)
        }
        else {
            let p = document.createElement('p')
            p.className = 'p'
            p.innerHTML = "אין לך פניות קודמות"
            document.getElementById('contact').appendChild(p)
        }
    }
    // document.getElementById("message").innerHTML = req.responseText
}

//רישום לקוח חדש 
function signUpUser() {
    out()
    let arr = []
    for (i = 0; i < 7; i++)
        arr[i] = document.getElementById(i).value;
    if (arr[arr.length - 1] !== arr[arr.length - 2]) {
        document.querySelector('#signUp').value = "נא הכנס סיסמא זהה"
    }
    else {
        const customer = new Customer(arr[4], arr[5], arr[0], arr[1], arr[2], arr[3]);
        //todo: להגדיר מחלקשה של אירוע ולמלא את הנתונים לשליחה
        // הכנסת הקובץ והאירוע לתוך משתנה
        let data = new FormData();
        data.append('customer', JSON.stringify(customer));

        //יצירת קריאה חדשה
        var xhr = new XMLHttpRequest();
        //פתיחת הקריאה - סוג וכתובת
        xhr.open('POST', 'https://localhost:44397/API/customer/AddNewCustomer/', true);
        xhr.onload = function () {
            // do something to response
            if (xhr.responseText === 'true') {
                localStorage.setItem('isNewUser', true)
                localStorage.setItem('gmail', arr[3])
                localStorage.setItem('username', arr[4])
                localStorage.setItem('pass', arr[5])
                window.location.href = "index.html"
            }
            else {
                document.getElementById('ex').className += "bottom-container"
                document.querySelector('.btn').innerHTML = "משתמש זה כבר קיים במערכת, להתחברות לחץ כאן"
                localStorage.setItem('name', arr[4])
            }
        };
        //POSTצירוף המשתנים לתוך ה
        xhr.send(data);
    }
}

// הצגת כל האירועים
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
        for (let i = arr.length - 1; i >= 0; i--) {
            console.log(new Date().getFullYear());
            let dateEv = new Date(arr[i].EventDate)
            let today = new Date();
            if (dateEv < today)
                arr[i] = null
            // if (arr[i] != null) {
            //     arr[0] = arr[i]
            //     i = -1
            // }
        }
        for (let i = 0; i < arr.length; i++) {
            if (arr[i] != null) {
                let card2 = document.createElement('div')
                card2.setAttribute('class', 'card2')
                let new_a = document.createElement('a')
                new_a.onclick = function () { cards(arr[i].Id) }
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
                price.innerHTML = `${arr[i].EventDate.slice(11, 16)}`;
                new_a.appendChild(price)
                let description = document.createElement('p')
                description.setAttribute('class', 'description')
                description.innerHTML = `${arr[i].Description}`;
                new_a.appendChild(description)
                //add a p - few on the event
                let btn = document.createElement('button')
                btn.innerHTML = "הוסף לעגלה"
                btn.onclick = function () { buyCard(arr[i].Id, false) }
                card.appendChild(new_a)
                card.appendChild(btn)
                card2.appendChild(card)
                document.getElementById('allEvent').appendChild(card2)
            }
        }
    }
    if (str === "AllTrip/") {
        allEvent("AllSpeache/")
        init()
    }
}

//הצגת תאריך בפורמט יפה
function appendLeadingZeros(int) {
    if (int < 10) {
        return '0' + int;
    }
    return int;
}

//הוספת כרטיס לעגלה
function buyCard(idEvent, flag) {
    //הוספת כרטיס לעגלה
    let req = new XMLHttpRequest()
    req.open('get', 'https:/localhost:44397/API/customer/AddEventToCart/' + idEvent + '/' + localStorage.getItem('gmail') + '/' + 0, true);
    req.send()
    // צריך לבדוק למה הוא לא נכנס לתוך הפונק'
    req.onload = function () {
        let req2 = new XMLHttpRequest()
        req2.open('get', 'https:/localhost:44397/API/mainEvents/RemainingTickets/' + idEvent + '/' + 1, true);
        req2.send()
        // צריך לבדוק למה הוא לא נכנס לתוך הפונק'
        req2.onload = function () {
            let req3 = new XMLHttpRequest()
            req3.open('get', 'https://localhost:44397/API/mainEvents/CountCard/' + localStorage.getItem('gmail') + '/' + idEvent, true)
            req3.send()
            req3.onload = function () {
                // return req.responseText
                count = req3.response
                if (req2.response - count >= 1) {
                    console.log("add to cart")
                    if (!flag)
                        alert("נוסף לעגלה")
                    location.reload()
                }
                else alert("הכרטיסים אזלו מהמלאי")
            }
        }
    }
}

//רישמת מס' הפריטים בעגלה
function prudctInCart() {
    let req = new XMLHttpRequest()
    req.open('get', 'https:/localhost:44397/API/customer/GetAllEventInCart/' + localStorage.getItem('gmail') + '/' + 0, true);
    req.send()
    req.onload = function () {
        let numProduct = document.createElement('div')
        numProduct.setAttribute('class', 'numProduct')
        numProduct.innerHTML = JSON.parse(req.response).length
        document.getElementsByClassName('cart').appendChild(numProduct)
    }
}

//החזרת כל הכרטיסים בעגלה
// function allEventsForCustomer() {
//     let req = new XMLHttpRequest()
//     req.open('get', 'https://localhost:44397/API/customer/GetAllEventForCust/' + localStorage.getItem('gmail') + '/' + 0, true)
//     req.send()
//     req.onload = function () {
//         return JSON.parse(req.response)
//     }
// }

//החזרת סוג האירוע
function typeEvent(idEvent) {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/typeEvent/' + idEvent, true)
    req.send()
    req.onload = function () {
        return req.responseText
    }
}

//מס' הכרטיסים מאותו אירוע
function countCard(idEvent, count, totalPrice, price) {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/CountCard/' + localStorage.getItem('gmail') + '/' + idEvent, true)
    req.send()
    req.onload = function () {
        // return req.responseText
        count.value = req.responseText
        totalPrice.innerHTML = count.value * price.textContent
    }
}

function tempInsert1(idEvent) {
    let req2 = new XMLHttpRequest()
    req2.open('get', 'https://localhost:44397/API/mainEvents/typeEvent/' + idEvent, true)
    req2.send()
    req2.onload = function () {
        let type = req2.responseText
        console.log(type.slice(1, type.length - 1));
        type = type.slice(1, type.length - 1) + 'Prophil/'
        let item = document.createElement('div')
        item.setAttribute('class', 'item')
        tempInsert(type, idEvent, item)
    }
}

//ביטול כרטיס לאירוע
function delteCard(idEvent) {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/EventProphil/' + idEvent, true)
    req.send()
    req.onload = function () {
        const MainEvent = JSON.parse(req.response)
        let req2 = new XMLHttpRequest()
        req2.open('get', 'https://localhost:44397/API/mainEvents/CardCancellation/' + localStorage.getItem('gmail') + '/' + idEvent, true)
        req2.send()
        req2.onload = function () {
            // if (req2.response === "true")
            //     alert("הכרטיס לאירוע בוטל בהצלחה, תקבל החזר בתוך 2 ימי עסקים")
            alert("הכרטיס הוסר בהצלחה!!")
            location.reload()
        }
    }
}

function delteCards(idEvent) {
    let count
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/CountCard/' + localStorage.getItem('gmail') + '/' + idEvent, true)
    req.send()
    req.onload = function () {
        count = req.responseText
        let i = 0
        for (; i < count; i++) {
            let req2 = new XMLHttpRequest()
            req2.open('get', 'https://localhost:44397/API/mainEvents/EventProphil/' + idEvent, true)
            req2.send()
            req2.onload = function () {
                const MainEvent = JSON.parse(req2.response)
                let req3 = new XMLHttpRequest()
                req3.open('get', 'https://localhost:44397/API/mainEvents/CardCancellation/' + localStorage.getItem('gmail') + '/' + idEvent, true)
                req3.send()
                req3.onload = function () {
                    if (i == count)
                        location.reload()
                }
            }
        }
    }
}

//לשנות את הסטטוס לכל הכרטיסים שלו לאמת
function payment() {
    let req2 = new XMLHttpRequest()
    req2.open('get', 'https://localhost:44397/API/mainEvents/BuyCard/' + localStorage.getItem('gmail') + '/' + 0, true)
    req2.send()
    req2.onload = function () {
        window.location.href = "cart.html"
    }
}


function tempInsert(type, idEvent, item) {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/' + type + idEvent, true)
    req.send()
    req.onload = function () {
        const ev = JSON.parse(req.responseText)
        let buttons = document.createElement('div')
        buttons.setAttribute('class', 'buttons')
        let aa = document.createElement('a')
        aa.onclick = function () { delteCards(ev.Id) }
        let deleteBtn = document.createElement('span')
        deleteBtn.setAttribute('class', 'delete-btn')
        aa.appendChild(deleteBtn)
        buttons.appendChild(aa)
        // let likeBtn = document.createElement('span')
        // likeBtn.setAttribute('class', 'like-btn')
        // likeBtn.onclick = deleteCard(ev.Id)
        // buttons.appendChild(likeBtn)
        item.appendChild(buttons)
        let image = document.createElement('div')
        image.setAttribute('class', 'imageCart')
        let img = document.createElement('img')
        img.src = ev.ImagePath
        image.appendChild(img)
        item.appendChild(image)
        let description = document.createElement('div')
        description.setAttribute('class', 'description')
        let name = document.createElement('span')
        name.textContent = ev.Name
        description.appendChild(name)
        let price = document.createElement('span')
        price.textContent = ev.CostPerParticipance
        description.appendChild(price)
        item.appendChild(description)
        let quantity = document.createElement('div')
        quantity.setAttribute('class', 'quantity')
        let plusBtn = document.createElement('button')
        plusBtn.setAttribute('class', 'plus-btn')
        plusBtn.type = "button"
        plusBtn.onclick = function () { buyCard(ev.Id, true) }
        // plusBtn.value = "+"
        let imgPlus = document.createElement('img')
        imgPlus.src = 'https://designmodo.com/demo/shopping-cart/plus.svg'
        plusBtn.appendChild(imgPlus)
        quantity.appendChild(plusBtn)
        let count = document.createElement('input')
        count.type = "text"
        count.setAttribute('class', 'count')
        let totalPrice = document.createElement('div')
        totalPrice.setAttribute('class', 'total-price')
        countCard(ev.Id, count, totalPrice, price)
        quantity.appendChild(count)
        let minusBtn = document.createElement('button')
        minusBtn.setAttribute('class', 'minus-btn')
        minusBtn.type = "button"
        minusBtn.onclick = function () { delteCard(ev.Id) }
        let imgMinus = document.createElement('img')
        imgMinus.src = 'https://designmodo.com/demo/shopping-cart/minus.svg'
        minusBtn.appendChild(imgMinus)
        quantity.appendChild(minusBtn)
        item.appendChild(quantity)
        item.appendChild(totalPrice)
        // prudctInCart()
        document.getElementById('shopping-cart').appendChild(item)
    }
}


//הצגת הכרטיסים בעגלה
function cart() {
    init()
    let req1 = new XMLHttpRequest()
    req1.open('get', 'https://localhost:44397/API/customer/GetAllEventInCart/' + localStorage.getItem('gmail') + '/' + 0, true)
    req1.send()
    req1.onload = function () {
        let vec = JSON.parse(req1.response)
        for (let j = 0; j < vec.length; j++)
            for (let k = j + 1; k < vec.length; k++)
                if (vec[j] == vec[k])
                    vec[k] = -1
        let arr = []
        let temp = 0
        for (let j = 0; j < vec.length; j++)
            if (vec[j] != -1)
                arr[temp++] = vec[j]
        console.log(arr);
        for (let i = 0; i < arr.length; i++) {
            tempInsert1(arr[i])
        }
        if (vec.length > 0) {
            let a = document.createElement('a')
            a.href = "payment.html"
            a.text = '<<<לתשלום'
            a.onclick = function () { isEmpty() }
            a.setAttribute('class', 'payment')
            document.getElementById('pay').appendChild(a)
        }
    }
}

function isEmpty() {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/mainEvents/BuyCard/' + localStorage.getItem('gmail') + '/' + 0, true)
    req.send()
    req.onload = function () {

    }
}

function success() {
    window.location.href = "sucsess.html"
}

function bdika() {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/contacs/GetUserQuetions/' + "t@tta", true)
    req.send()
    req.onload = function () {

        users = JSON.parse(req.response);
        console.log(users);

        let table = document.createElement('table')
        table.setAttribute('class', 'table table-primary table-bordered table-hover')

        //שורת כותרת
        let r = document.createElement('tr');
        //עמודות כותרת
        let d1 = document.createElement('th');
        d1.innerHTML = "שאלה"

        r.appendChild(d1)

        d1 = document.createElement('th');
        d1.innerHTML = "תשובה"

        // r.appendChild(d1)
        // d1 = document.createElement('th');
        // d1.innerHTML = " כתובת"

        // r.appendChild(d1)
        // d1 = document.createElement('th');
        // d1.innerHTML = "טלפון"

        r.appendChild(d1)

        // הוספת שורת כותרת
        table.appendChild(r)

        //שורות תוכן
        for (let i = 0; i < users.length; i++) {
            let r = document.createElement('tr');
            //עמודות תוכן
            //שם פרטי
            let d1 = document.createElement('td');
            d1.innerHTML = users[i].Question

            r.appendChild(d1)

            //שם משפחה
            d1 = document.createElement('td');
            d1.innerHTML = users[i].Answer

            r.appendChild(d1)

            // //כתובת 
            // d1 = document.createElement('td');
            // d1.innerHTML = users[i].Address

            // r.appendChild(d1)

            // //טלפון
            // d1 = document.createElement('td');
            // d1.innerHTML = users[i].Phone

            // r.appendChild(d1)

            // הוספת שורות תוכן
            table.appendChild(r)

        }

        document.getElementById('contact').appendChild(table)
    }
    // document.getElementById("message").innerHTML = req.responseText
}

function bdika1() {
    let req = new XMLHttpRequest()
    req.open('get', 'https://localhost:44397/API/contacs/AddAnswerToQuestion/' + 1 + '/' + "hgggghghh", true)
    req.send()
    req.onload = function () {

        users = JSON.parse(req.response);
        console.log(users);

        let table = document.createElement('table')
        table.setAttribute('class', 'table table-primary table-bordered table-hover')

        //שורת כותרת
        let r = document.createElement('tr');
        //עמודות כותרת
        let d1 = document.createElement('th');
        d1.innerHTML = "שאלה"

        r.appendChild(d1)

        d1 = document.createElement('th');
        d1.innerHTML = "תשובה"

        // r.appendChild(d1)
        // d1 = document.createElement('th');
        // d1.innerHTML = " כתובת"

        // r.appendChild(d1)
        // d1 = document.createElement('th');
        // d1.innerHTML = "טלפון"

        r.appendChild(d1)

        // הוספת שורת כותרת
        table.appendChild(r)

        //שורות תוכן
        for (let i = 0; i < users.length; i++) {
            let r = document.createElement('tr');
            //עמודות תוכן
            //שם פרטי
            let d1 = document.createElement('td');
            d1.innerHTML = users[i].Question

            r.appendChild(d1)

            //שם משפחה
            d1 = document.createElement('td');
            d1.innerHTML = users[i].Answer

            r.appendChild(d1)

            // //כתובת 
            // d1 = document.createElement('td');
            // d1.innerHTML = users[i].Address

            // r.appendChild(d1)

            // //טלפון
            // d1 = document.createElement('td');
            // d1.innerHTML = users[i].Phone

            // r.appendChild(d1)

            // הוספת שורות תוכן
            table.appendChild(r)

        }

        document.getElementById('contact1').appendChild(table)
    }
    // document.getElementById("message").innerHTML = req.responseText
}
//להעביר למנהל
function changeImage() {
    const selectedFile = document.getElementById('myFile').files[0];
    console.log(selectedFile);
    localStorage.setItem('src', selectedFile.name)
    window.location.href = "aaa.html"
}

function start() {
    let img = document.createElement('img')
    img.src = localStorage.getItem('src')
    document.getElementById('in').appendChild(img)


}

//הצגת כל הפניות והאפשרות להכניס להם תשובה
// function allQuestion() {
//     let req = new XMLHttpRequest();
//     //פתיחת הקריאה - סוג וכתובת
//     req.open('get', 'https:/localhost:44397/API/contacs/GetAllQuetions/', true);
//     //שליחת הבקשה
//     req.send();
//     req.onload = function () {
//         console.log(req.responseText);
//         let allRequest = JSON.parse(req.response);
//         for (let i = 0; i < allRequest.length; i++) {
//             let new_btn = document.createElement('button')
//             new_btn.value = allRequest[i].contactId
//             new_btn.innerHTML = allRequest[i].Question
//             new_btn.className = "accordion"
//             let new_div = document.createElement('div')
//             new_div.className = "panel"
//             document.getElementById('contact').appendChild(new_btn)
//             document.getElementById('contact').appendChild(new_div)
//             if (allRequest[i].Answer !== null) {
//                 let new_p = document.createElement('p')
//                 new_p.innerHTML = allRequest[i].Answer
//                 new_div.appendChild(new_p)
//             }
//             else {
//                 let input = document.createElement('input')
//                 input.type = "text"
//                 input.className = "answer"
//                 let new_btn1 = document.createElement('input')
//                 new_btn1.type = "button"
//                 new_btn1.value = "שליחה"
//                 new_btn1.onclick = function () { insertAnswer(new_btn.value, input.value) }
//                 new_div.appendChild(input)
//                 new_div.appendChild(new_btn1)
//             }
//         }
//         let question = document.getElementsByClassName("accordion");
//         for (let i = 0; i < question.length; i++) {
//             question[i].addEventListener("click", function () {
//                 /* Toggle between adding and removing the "active" class,
//                 to highlight the button that controls the panel */
//                 this.classList.toggle("active");

//                 /* Toggle between hiding and showing the active panel */
//                 var panel = this.nextElementSibling;
//                 if (panel.style.display === "block") {
//                     panel.style.display = "none";
//                 } else {
//                     panel.style.display = "block";
//                 }
//             });
//         }
//     }
// }

// //הכנסת התשובה לסי שארפ וגם רענון הדף
// function insertAnswer(contactId, answer) {
//     let req = new XMLHttpRequest();
//     //פתיחת הקריאה - סוג וכתובת
//     req.open('get', 'https:/localhost:44397/API/contacs/AddAnswerToQuestion/' + contactId + '/' + answer, true);
//     //שליחת הבקשה
//     req.send();
//     req.onload = function () {
//         if (req.responseText)
//             window.location.href = "allQoustion.html"
//     }
// }
