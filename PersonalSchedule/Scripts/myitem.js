var item_str = "{\"body\":[[\"2\",\"1234\"],[\"i\",\"../../img/1.png\"],[\"p\",\"一段话\"],[\"i\",\"../../img/testyourname1.jpg\"]]}"

var write_maintext = function (item_str) {
    var page_item = JSON.parse(item_str);
    maintext = document.getElementById('maintext');
    maintext.innerHTML = "";
    title = document.createElement('h1');
    title.textContent = document.title;
    maintext.appendChild(title);
    page_item_list = page_item.body;
    page_item_list.forEach(function (body_item_str) {
        //console.log(body_item_str);
        if (body_item_str == null || body_item_str == '') {
            body_item_str = ' ';
        }
        //console.log(body_item_str);
        body_item_markup = body_item_str[0];
        body_item_text = body_item_str[1];
        //console.log(body_item_markup);
        //console.log(body_item_text);
        if (body_item_markup == '2') {
            body_item = document.createElement('h2');
            body_item.textContent = body_item_text;
            maintext.appendChild(body_item);
        }
        else if (body_item_markup == 'i') {
            body_item = document.createElement('img');
            body_item.setAttribute('src', body_item_text);
            body_item.setAttribute('class', 'img-responsive center-block');
            maintext.appendChild(body_item);
        }
        else {
            body_item = document.createElement('p');
            body_item.textContent = body_item_text;
            maintext.appendChild(body_item);
        }
    });
}