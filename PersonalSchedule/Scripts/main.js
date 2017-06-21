;(function () {

	'use strict';



	// iPad and iPod detection
	var isiPad = function(){
		return (navigator.platform.indexOf("iPad") != -1);
	};

	var isiPhone = function(){
	    return (
			(navigator.platform.indexOf("iPhone") != -1) ||
			(navigator.platform.indexOf("iPod") != -1)
	    );
	};

	// Parallax
	var parallax = function() {
		$(window).stellar();
	};



	// Burger Menu
	var burgerMenu = function() {

		$('body').on('click', '.js-fh5co-nav-toggle', function(event){

			event.preventDefault();

			if ( $('#navbar').is(':visible') ) {
				$(this).removeClass('active');
			} else {
				$(this).addClass('active');
			}



		});

	};


	var goToTop = function() {

		$('.js-gotop').on('click', function(event){

			event.preventDefault();

			$('html, body').animate({
				scrollTop: $('html').offset().top
			}, 500);

			return false;
		});

	};


	// Page Nav
	var clickMenu = function() {

		$('#navbar a:not([class="external"])').click(function(event){
			var section = $(this).data('nav-section'),
				navbar = $('#navbar');

				if ( $('[data-section="' + section + '"]').length ) {
			    	$('html, body').animate({
			        	scrollTop: $('[data-section="' + section + '"]').offset().top
			    	}, 500);
			   }

		    if ( navbar.is(':visible')) {
		    	navbar.removeClass('in');
		    	navbar.attr('aria-expanded', 'false');
		    	$('.js-fh5co-nav-toggle').removeClass('active');
		    }

		    event.preventDefault();
		    return false;
		});


	};

	// Reflect scrolling in navigation
	var navActive = function(section) {

		var $el = $('#navbar > ul');
		$el.find('li').removeClass('active');
		$el.each(function(){
			$(this).find('a[data-nav-section="'+section+'"]').closest('li').addClass('active');
		});

	};

	var navigationSection = function() {

		var $section = $('section[data-section]');

		$section.waypoint(function(direction) {

		  	if (direction === 'down') {
		    	navActive($(this.element).data('section'));
		  	}
		}, {
	  		offset: '150px'
		});

		$section.waypoint(function(direction) {
		  	if (direction === 'up') {
		    	navActive($(this.element).data('section'));
		  	}
		}, {
		  	offset: function() { return -$(this.element).height() + 155; }
		});

	};





	// Window Scroll
	var windowScroll = function() {
		var lastScrollTop = 0;

		$(window).scroll(function(event){

		   	var header = $('#fh5co-header'),
				scrlTop = $(this).scrollTop();

			if ( scrlTop > 500 && scrlTop <= 2000 ) {
				header.addClass('navbar-fixed-top fh5co-animated slideInDown');
			} else if ( scrlTop <= 500) {
				if ( header.hasClass('navbar-fixed-top') ) {
					header.addClass('navbar-fixed-top fh5co-animated slideOutUp');
					setTimeout(function(){
						header.removeClass('navbar-fixed-top fh5co-animated slideInDown slideOutUp');
					}, 100 );
				}
			}

		});
	};



	// Animations

	var contentWayPoint = function() {
		var i = 0;
		$('.animate-box').waypoint( function( direction ) {

			if( direction === 'down' && !$(this.element).hasClass('animated') ) {

				i++;

				$(this.element).addClass('item-animate');
				setTimeout(function(){

					$('body .animate-box.item-animate').each(function(k){
						var el = $(this);
						setTimeout( function () {
							el.addClass('fadeInUp animated');
							el.removeClass('item-animate');
						},  k * 200, 'easeInOutExpo' );
					});

				}, 100);

			}

		} , { offset: '85%' } );
	};

    // Document on load.
	$(function(){

		// parallax();

		// burgerMenu();

		// clickMenu();

		// windowScroll();

		// navigationSection();

		// goToTop();


		// Animations
		contentWayPoint();
		
	});


}());
function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function() {
            oldonload();
            func();
        }
    }
}

function insertAfter(newElement,targetElement) {
    var parent = targetElement.parentNode;
    if (parent.lastChild == targetElement) {
        parent.appendChild(newElement);
    } else {
        parent.insertBefore(newElement,targetElement.nextSibling);
    }
}

function addCookie(name, value, expireHours){
	var cookieString = name + "=" + escape(value);

	var date = new Date();
	
	if(expireHours > 0){	
		date.setTime(date.getTime() + expireHours * 3600 * 1000);
		cookieString = cookieString + ";expire=" + date.toGMTString() + ";path=/VoteAndVoice;domain=localhost";
	}
	document.cookie = cookieString;
}
//获取cookie
function getCookie(name){
	var strCookie = document.cookie;
	var arrCookie = strCookie.split("; ");
	for(var i = 0; i < arrCookie.length; ++ i){
		var arr = arrCookie[i].split("=");
		if(arr[0] == name) return arr[1];
	}
	return "";
}


/*************************************index.html****************************/
function prepareClick()
{
    var links = document.getElementsByClassName("modal-btn");
    for(var i = 0; i < links.length; ++i){
        links[i].onclick = function(){
            $('#login').modal('hide');
            $('#register').modal('hide');
            $('#charge').modal('hide');
            $('#modal_preview').modal('hide');
            return false;
        }
    }

}
addLoadEvent(prepareClick);

/**************************basicInfo.html*******************************/
function prepareSidebarLink(){
    var sidebar = document.getElementById("function-list");
    if(sidebar == null) return;
    var protocol = window.location.protocol;
    var host = window.location.host;
    var url = protocol + host;
    //for the sidebar
    var links = sidebar.getElementsByClassName("user-button");
    if(links[0].getElementsByTagName("a")[0] == null) return;
    links[0].getElementsByTagName("a")[0].setAttribute("href", "/VoteAndVoice/BasicInfo");
    links[1].getElementsByTagName("a")[0].setAttribute("href",  "MyQuestionnaire");
    links[2].getElementsByTagName("a")[0].setAttribute("href", "Safe");

    //for the toggle
    var liLink = sidebar.getElementsByTagName("li");
    liLink[0].getElementsByTagName("a")[0].setAttribute("href", "/VoteAndVoice/latestNews");
    liLink[1].getElementsByTagName("a")[0].setAttribute("href", "/VoteAndVoice/MyFriend");
    liLink[2].getElementsByTagName("a")[0].setAttribute("href", "/VoteAndVoice/IFollow");
    liLink[3].getElementsByTagName("a")[0].setAttribute("href", "/VoteAndVoice/FollowMe");
    liLink[4].getElementsByTagName("a")[0].setAttribute("href", "/VoteAndVoice/searchUser")
}
addLoadEvent(prepareSidebarLink);
function editUserInfo(type)
{
    var infoForm = document.getElementById("b-info");
    var inputItem = infoForm.getElementsByTagName("input");
    for(var i = 0;i < inputItem.length; ++ i) {
        if(type == true) {
            inputItem[i].disabled = false;
            inputItem[i].style.border = "solid #1ABC9C 1px";
        }
        else{
            inputItem[i].disabled = "disabled";
            inputItem[i].style.border = "none";
        }
    }
    var btn = infoForm.getElementsByTagName("button")[0];
    if(type == true)
    {
        btn.innerText = "确定";
        btn.setAttribute("type", "submit");
        btn.setAttribute("class", "btn btn-success");
        btn.setAttribute("onclick", "");
    }
    else{
        btn.innerText = "修改";
        btn.setAttribute("class", "btn btn-warning");
        btn.onclick = function(){
            editUserInfo(true);
        }
    }
}
/********************************creatingQuestionnaire.html*******************************************/
/********************************creatingQuestionnaire.html*******************************************/
var is_click = false;
var is_a_click = false;
var txtNode;
var tagdiv;
//选择类型
function onClickType(btn) {
    if(btn.is_click == "false" && is_click == false){
        //改变button的样式
        btn.is_click = "true"
        is_click = true;
        btn.style.backgroundColor = "#f0735a";
        //增添标签
        var question = document.getElementById("type-title");
        txtNode = document.createTextNode(btn.innerText);
        question.appendChild(txtNode);

        tagdiv = document.createElement("form");
        tagdiv.setAttribute("action", "CreateQuestionnaire_authority");///
        tagdiv.setAttribute("method", "post");///
        var type = document.createElement("input");///
        type.setAttribute("type", "hidden");///
        type.setAttribute("name", "type");///
        type.setAttribute("value", btn.innerText);///
        var tag = document.createElement("input");
        tag.setAttribute("placeholder", "为您的问卷添加一个tag，说明其内容吧^_^");
        tag.setAttribute("class", "tag");
        tag.setAttribute("name", "tag");///
        var submit = document.createElement("button");
        submit.setAttribute("class", "btn btn-success");
        submit.setAttribute("id", "tag-btn");
        submit.setAttribute("type", "submit");///
        submit.appendChild(document.createTextNode("提交"));
        tagdiv.appendChild(type);///
        tagdiv.appendChild(tag);
        tagdiv.appendChild(submit);
        insertAfter(tagdiv, question);
    }
    else if(is_click == true && btn.is_click == "true"){
        btn.is_click = "false";
        btn.style.backgroundColor = "white";
        is_click = false;

        var question = document.getElementById("type-title");
        question.parentNode.removeChild(tagdiv);
        question.removeChild(txtNode);
        //取消链接
        var first_link = document.getElementsByClassName("first-link");
        for(var i = 0;i < first_link.length; ++ i){
            first_link[i].setAttribute("href", "#");
        }
    }
}

function prepareClickType(){
    if(document.getElementsByClassName("qtype") == null) return;
    var qtype = document.getElementsByClassName("qtype");
    var links = [];
    for(var j = 0;j < qtype.length; ++ j){
        var row = qtype[j].getElementsByTagName("button");
        for(var k = 0; k < row.length; ++ k){
            links[links.length] = row[k];
        }
    }
    if(links == null) return false;

    for(var i = 0;i < links.length; ++ i){
        links[i].is_click = "false";
        links[i].onclick = function() {
            onClickType(this);
        };
        links[i].onkeypress = links[i].onclick;
    }
}
//选择权限
function onClickAuthority(btn){
    if(btn.is_click == "false" && is_a_click == false){
        btn.is_click = "true";
        is_a_click = true;
        var question = document.getElementById("type-title");
        txtNode = document.createTextNode(btn.innerText);
        question.appendChild(txtNode);
        document.getElementById("form-authority").setAttribute("value", btn.innerText);///
    }
    else if(is_a_click == true && btn.is_click == "true"){
        btn.is_click = "false";
        is_a_click = false;
        var question = document.getElementById("type-title");
        question.removeChild(txtNode);
        document.getElementById("form-authority").setAttribute("value", "");///
    }
}
function prepareClickAuthority(){
    var links = [];
    if(document.getElementById("public-authority") == null || document.getElementById("private-authority") == null) return;
    var pub = document.getElementById("public-authority").getElementsByTagName("button")[0];
    var pri = document.getElementById("private-authority").getElementsByTagName("button")[0];
    links[0] = pub;
    links[1] = pri;
    for(var i = 0; i < links.length; ++ i){
        links[i].is_click = "false";
        links[i].onclick = function(){
            onClickAuthority(this);
        };
        links[i].onkeypress = links[i].onclick;
    }
}
//For the page which to choose the friends
function prepareClickAdd(){
    var fadd = [];
    if(document.getElementById("friend-list") == null) return;
    var fadd = document.getElementById("friend-list").getElementsByTagName("button");

    for(var i = 0; i < fadd.length; ++ i){
        fadd[i].onclick = function(){
            onClickAdd(this);
        }
    }
}
addLoadEvent(prepareClickAuthority);
addLoadEvent(prepareClickType);
/*************************creatingQuestionnaire4.html****************************************/
var cpeople = [];
function assignJson(name, is_add){
    var chose = document.getElementById("people_chose");
    if(is_add == true){
        cpeople.push(name);
    }
    else{
        var i = 0;
        var temp = [];
        for( ;i < cpeople.length; ++ i) {
            if (cpeople[i] == name) {
                break;
            }
            else temp[i] = cpeople[i];
        }
        for(;i < cpeople.length - 1; ++ i){
            temp[i] = cpeople[i + 1];
        }
        cpeople = temp;
    }
    var arrString = "[";
    for(var i = 0; i < cpeople.length; ++ i){
        if(i != cpeople.length - 1) arrString += '"' + cpeople[i] + '"' +  ",";
        else arrString += '"' + cpeople[i] + '"';
    }
    arrString += "]";
    var jsonString = arrString;//JSON.stringify(arrString);
    alert(jsonString);
    chose.value = jsonString;
}
function onClickAdd(btn){
    //开头进行判断
    if(btn.is_click == "true") return;
    else btn.is_click = "true";

    var txt = btn.parentNode.innerText;
    var chose = document.createElement("span");
    var pdiv = document.getElementById("choose");

    txt = txt.substring(0, txt.length - 4);
    var txtNode = document.createTextNode(txt);
    assignJson(txt, true);

    chose.setAttribute("class", "btn btn-default chose");
    chose.appendChild(txtNode);
    chose.onclick = function(){
        assignJson(txt, false);
        btn.is_click = "false";
        onClickChose(this);
    }
    insertAfter(chose, pdiv.lastElementChild);
}
function onClickChose(btn){
    var pdiv = document.getElementById("choose");
    pdiv.removeChild(btn);
}
addLoadEvent(prepareClickAdd);

/*************************constructingQuestionnaire.html**************************************/
//让sidebar随着滚动条一起滚动
$(document).scroll(function (){

    //固定SideBar
    if ($(document).scrollTop() > '180') {
        $('#questionType').offset({top:$(document).scrollTop()+10});
    }else if($(document).scrollTop() <= '180') {
        $('#questionType').offset({top:191});
    };

});
//逻辑设置
function setLogic(item){

}
//复制
function questionCopy(item){
    var out = item.parentNode.parentNode.parentNode;
    if(out == null) return;
    var copy = out.cloneNode(true);
    var qorder = copy.getElementsByClassName("qorder")[0];
    qorder.innerText = question_num;
    var li_list = copy.getElementsByTagName("li");
    bindHandle(li_list);
    insertAfter(copy, out);
    var order_list = questionnaire.getElementsByClassName("qorder");
    if(order_list == null) return;
    for(var i = 0;i < order_list.length; ++ i){
        order_list[i].innerText = i + 1;
    }
}
//绑定操作
function bindHandle(li_list)
{
    if(li_list == null ) return;
    li_list[0].onclick = function(){
        questionUp(this, true);
    }
    li_list[1].onclick = function(){
        questionUp(this, false);
    }
    li_list[2].onclick = function(){
        setLogic(this);
    };
    li_list[3].onclick = function(){
        ++ question_num;
        questionCopy(this);
    };
    li_list[4].onclick = function(){
        -- question_num;
        questionDelete(this);
    };
}
//删除
function questionDelete(item){
    var out = item.parentNode.parentNode.parentNode;
    if(out == null) return;
    var questionnaire = document.getElementById("questionnaire");
    questionnaire.removeChild(out);

    var order_list = questionnaire.getElementsByClassName("qorder");
    if(order_list == null) return;
    for(var i = 0;i < order_list.length; ++ i){
        order_list[i].innerText = i + 1;
    }
}
//增加问题
var question_num = 0;
function prepareQuestionType() {
    var single = document.getElementById("single");
    var multiple = document.getElementById("multiple");
    var qanda = document.getElementById("qanda");
    var preview = document.getElementById("preview");
    if (single == null || multiple == null || qanda == null || preview == null) return;
    single.onclick = function () {
        ++ question_num;
        onClickSingle_Mulitple(true);
    };
    multiple.onclick = function () {
        ++ question_num;
        onClickSingle_Mulitple(false);
    };
    qanda.onclick = function () {
        ++ question_num;
        onClickQandA();
    };
    preview.onclick = function(){
        onClickPreview();
    }
}
addLoadEvent(prepareQuestionType);

//增加单选题或者多选题
function onClickSingle_Mulitple(is_single){
    var questionnaire = document.getElementById("questionnaire");
    if(questionnaire == null) return;

    var out = document.createElement("div");
    if(is_single == true) {
        out.setAttribute("class", "qnaire-out qnaire-single-question");
    }
    else{
        out.setAttribute("class", "qnaire-out qnaire-multiple-question");
    }
    var inside = document.createElement("div");
    inside.setAttribute("class", "qnaire-out-inside");

    var order = document.createElement("span");
    order.setAttribute("class", "qorder");
    order.appendChild(document.createTextNode(question_num));

    var title = document.createElement("div");
    title.setAttribute("contenteditable", "true");
    title.setAttribute("class", "qnaire qnaire-title");
    if(is_single == true) {
        title.appendChild(document.createTextNode("这是一个单选题"));
    }
    else {
        title.appendChild(document.createTextNode("这是一个多选题"));
    }
    var add = document.createElement("div");
    add.setAttribute("class", "add");
    var sign = document.createElement("span");
    if(is_single == true) {
        sign.onclick = function () {
            addOption(this, true);
        };
    }
    else{
        sign.onclick = function () {
            addOption(this, false);
        };
    }
    var operate = document.createElement("div");
    operate.setAttribute("class", "operate");
    var unorder = document.createElement("ul");
    var li_list = [];
    for(var i = 0; i < 5; ++ i){
        li_list[i] = document.createElement("li");
        unorder.appendChild(li_list[i]);
    }
    li_list[0].setAttribute("class", "question-up");
    li_list[0].setAttribute("title", "上移题目");

    li_list[1].setAttribute("class", "question-down");
    li_list[1].setAttribute("title", "下移题目");

    li_list[2].setAttribute("class", "set-logic");
    li_list[2].setAttribute("title", "逻辑设置");

    li_list[3].setAttribute("class", "question-copy");
    li_list[3].setAttribute("title", "复制");

    li_list[4].setAttribute("class", "question-delete");
    li_list[4].setAttribute("title", "删除");
    bindHandle(li_list);

    operate.appendChild(unorder);
    add.appendChild(sign);
    inside.appendChild(order);
    inside.appendChild(title);
    out.appendChild(inside);
    out.appendChild(add);
    out.appendChild(operate);
    questionnaire.appendChild(out);
    sign.onclick();
    sign.onclick();
}
//增加选择题选项
function addOption(btn, is_single){
    var option_out = document.createElement("div");
    var input = document.createElement("input");
    var option = document.createElement("div");
    var txtNode = document.createTextNode("新选项");

    option_out.setAttribute("class", "qnaire-out-inside");
    if(is_single == true) {
        input.setAttribute("type", "radio");
        input.setAttribute("name", "radio");
    }
    else{
        input.setAttribute("type", "checkbox");
        input.setAttribute("name", "checkbox");
    }
    option.setAttribute("class", "qnaire qnaire-option");
    option.setAttribute("contenteditable", "true");
    option.appendChild(txtNode);
    option_out.appendChild(input);
    option_out.appendChild(option);
    insertAfter(option_out, btn.parentNode.previousElementSibling);
}


//问答题
function onClickQandA(){
    var questionnaire = document.getElementById("questionnaire");
    if(questionnaire == null) return;

    var out = document.createElement("div");
    out.setAttribute("class", "qnaire-out qnaire-qanda-question");

    var inside = document.createElement("div");
    inside.setAttribute("class", "qnaire-out-inside");

    var order = document.createElement("span");
    order.setAttribute("class", "qorder");
    order.appendChild(document.createTextNode(question_num));

    var title = document.createElement("div");
    title.setAttribute("contenteditable", "true");
    title.setAttribute("class", "qnaire qnaire-title");
    title.appendChild(document.createTextNode("这是一个问答题"));

    var content = document.createElement("div");
    content.setAttribute("class", "qnaire-out-inside");
    var content_inside = document.createElement("div");
    content_inside.setAttribute("class", "qnaire qnaire-content");
    content_inside.setAttribute("contenteditable", "true");
    content.appendChild(content_inside);

    var add = document.createElement("div");
    add.setAttribute("class", "pse-add");
    var sign = document.createElement("span");

    var operate = document.createElement("div");
    operate.setAttribute("class", "operate");
    var unorder = document.createElement("ul");
    var li_list = [];
    for(var i = 0; i < 5; ++ i){
        li_list[i] = document.createElement("li");
        unorder.appendChild(li_list[i]);
    }
    li_list[0].setAttribute("class", "question-up");
    li_list[0].setAttribute("title", "上移题目");

    li_list[1].setAttribute("class", "question-down");
    li_list[1].setAttribute("title", "下移题目");

    li_list[2].setAttribute("class", "set-logic");
    li_list[2].setAttribute("title", "逻辑设置");

    li_list[3].setAttribute("class", "question-copy");
    li_list[3].setAttribute("title", "复制");

    li_list[4].setAttribute("class", "question-delete");
    li_list[4].setAttribute("title", "删除");
    bindHandle(li_list);

    operate.appendChild(unorder);
    add.appendChild(sign);
    inside.appendChild(order);
    inside.appendChild(title);
    out.appendChild(inside);
    out.appendChild(content);
    out.appendChild(add);
    out.appendChild(operate);
    questionnaire.appendChild(out);
}
//生成问题的json字符串
function generateJQuestion(question){
    var jquestion = "[";
	var qtitle = question.getElementsByClassName("qnaire-title")[0];
	qtitle = qtitle.innerText;
	jquestion += "'" + qtitle + "'"; 
	var options = question.getElementsByClassName("qnaire-option");
	if(options.length > 0) jquestion += ",";
	for(var i = 0; i < options.length; ++ i){
		if(i != options.length - 1) jquestion += "'" + options[i].innerText + "',";
		else jquestion += "'" + options[i].innerText + "'";
	}
	jquestion += "]";
	return jquestion;
}
//预览页面
function onClickPreview(){
	var title = document.getElementsByClassName("qnaire-main-title")[0];
	title = title.innerText;
	if(title == null || title == ""){
		document.getElementsByClassName("error-tips")[0].innerText = "*请填写标题";
		return;
	}
	else{
		document.getElementsByClassName("error-tips")[0].innerText = "";
	}
	var des = document.getElementsByClassName("qnaire-des")[0];
	des = des.innerText;
	//提示错误信息
	if(des == null || des == ""){
		document.getElementsByClassName("error-tips")[1].innerText = "*请填写描述";
		return;
	}
	else{
		document.getElementsByClassName("error-tips")[1].innerText = "";
	}
	
	var qnaire = document.getElementsByClassName("qnaire-out");
	var jorder = "{'order': [";
	var is_single = false;
	var jsingle = "{'single': [";
	var is_multiple = false;
	var jmultiple = "{'multiple': [";
	var is_qanda = false;
	var jqanda = "{'qanda': [";
	
	for(var i = 2;i < qnaire.length; ++ i){
		if(qnaire[i].getAttribute("class") == "qnaire-out qnaire-single-question"){
			if(is_single == false) is_single = true;
			else jsingle += ",";
			jorder += "'"+  'single' + "'";
			var jquestion = generateJQuestion(qnaire[i]);
			jsingle +=  jquestion;
		}
		else if(qnaire[i].getAttribute("class") == "qnaire-out qnaire-multiple-question"){
			if(is_multiple == false) is_multiple = true;
			else jmultiple += ",";
			jorder += "'" +  'multiple' + "'";
			var jquestion = generateJQuestion(qnaire[i]);
			jmultiple += jquestion;
		}
		else if(qnaire[i].getAttribute("class") == "qnaire-out qnaire-qanda-question"){
			if(is_qanda == false) is_qanda = true;
			else jqanda += ",";
			jorder += "'" + 'qanda' + "'";
			var jquestion = generateJQuestion(qnaire[i]);
			jqanda += jquestion;
		}
		if(i != qnaire.length - 1) jorder += ',';
	}
	jorder += "]}";
	jsingle += "]}";
	jmultiple += "]}";
	jqanda += "]}";
	//添加到cookie中
	console.log(jorder)///
	console.log(jsingle)///
	console.log(jmultiple)///
	console.log(jqanda)///
	addCookie('title', title, 1);
	addCookie('des', des, 1);
	addCookie('order', jorder, 1);
	addCookie('single', jsingle, 1);
	addCookie('multiple', jmultiple, 1);
	addCookie('qanda', jqanda, 1);
	
	window.open("/VoteAndVoice/previewQuestionnaire.jsp");
}

function processQnaire(order, single, multiple, qanda){
	var jorder = eval('(' + order + ')' );
	var jsingle = eval( '(' + single + ')' );
	var jmultiple =  eval( '(' + multiple + ')' );
	var jqanda = eval( '(' + qanda + ')' );
	console.log(order)///
	console.log(single)///
	console.log(multiple)///
	console.log(qanda)///
	var isingle = 0;
	var imultiple = 0;
	var iqanda = 0; 
	
	for(var i = 0; i < jorder.order.length; ++ i){
		switch(jorder.order[i]){
		case 'single':{
			var question = jsingle.single[isingle ++];
			addSingle(question, i + 1);
			break;
		}
		case 'multiple':{
			var question = jmultiple.multiple[imultiple ++];
			addMultiple(question, i + 1);
			break;
		}
		case 'qanda':{
			var question = jqanda.qanda[iqanda ++];
			addQandA(question, i + 1);
			break;
		}
		}
	}
}
//增加单选题
function addSingle(question, num){
	addSingleOrMultiple(question, num, "1");
}
//增加多选题
function addMultiple(question, num){
	addSingleOrMultiple(question, num, "2");
}
function addSingleOrMultiple(question, num, type){
	var root = document.createElement("div");
	root.setAttribute("class", "wjques maxtop question jqtransformdone");
	root.setAttribute("questiontype", type);
	
	var title = document.createElement("div");
	title.setAttribute("class", "title");
	
	var qorder = document.createElement("span");
	qorder.setAttribute("class", "qorder");
	qorder.appendChild(document.createTextNode(num + " "));
	
	title.appendChild(qorder);
	title.appendChild(document.createTextNode(question[0]));
	
	root.appendChild(title);
	
	var matrix = document.createElement("div");
	matrix.setAttribute("class","matrix");
	
	for(var i  = 1; i < question.length; ++ i){
		var checkbox = document.createElement("div");
		checkbox.setAttribute("class", "icheck_box");
		
		var wrapper = document.createElement("span");
		wrapper.setAttribute("class", "jqTransformRadioWrapper");
		
		var input =  document.createElement("input");
		if(type == "1"){
			input.setAttribute("type", "radio");
			var name = "typename" + num;
			input.setAttribute("name", name);
		}
		else if(type == "2") input.setAttribute('type', "checkbox");
		input.setAttribute("class", "option jqTransformHidden");
		
		wrapper.appendChild(input);
		wrapper.appendChild(document.createTextNode(" " + question[i]));
		
		checkbox.appendChild(wrapper);
		matrix.appendChild(checkbox);
	}
	root.appendChild(matrix);
	document.getElementById("question_box").appendChild(root);
}

//增加问答题
function addQandA(question, num){
	var root = document.createElement("div");
	root.setAttribute("class", "wjques maxtop question jqtransformdone");
	root.setAttribute("questiontype", "3");
	
	var title = document.createElement("div");
	title.setAttribute("class", "title");
	
	var qorder = document.createElement("span");
	qorder.setAttribute("class", "qorder");
	qorder.appendChild(document.createTextNode(num + " "));
	
	title.appendChild(qorder);
	title.appendChild(document.createTextNode(question[0]));
	
	root.appendChild(title);
	
	var matrix = document.createElement("div");
	matrix.setAttribute("class", "matrix");
	
	var textarea = document.createElement("textarea");
	textarea.setAttribute("class", "blankoption");
	textarea.setAttribute("cols", "40");
	textarea.setAttribute("rows", "5");
	
	matrix.appendChild(textarea);
	
	root.appendChild(matrix);
	document.getElementById("question_box").appendChild(root);
}
//上移和下移题目
function questionUp(btn, is_up){
    var question = btn.parentNode.parentNode.parentNode;
    if(question == null) return;
    var out = question.parentNode;
    if (is_up == true) {
        var prev = question.previousElementSibling;
        if (prev.getAttribute("class") == "qnaire-out qnaire-des") return;

        prev = prev.previousElementSibling;
        out.removeChild(question);
        insertAfter(question, prev);
    }
    else{
        var next = question.nextElementSibling;
        if(next == null) return;
        out.removeChild(question);
        insertAfter(question, next);
    }
    var order_list = out.getElementsByClassName("qorder");
    for(var i = 0; i < order_list.length; ++ i){
        order_list[i].innerText = i + 1;
    }
}
/*********************************************previewQuestionnaire************************/
function onSubmitQuestionnaire(){
	var confirm_btn  = document.getElementById("confirm_btn");
	if(confirm_btn ==  null) return false;
	confirm_btn.onclick = function(){
		var title = unescape(getCookie('title'));
		var des = unescape(getCookie('des'));
		var order = unescape(getCookie('order'));
		var single = unescape(getCookie('single'));
		var multiple = unescape(getCookie('multiple'));
		var qanda = unescape(getCookie('qanda'));
		
		$("#title").val(title);
		$("#des").val(des);
		$("#order").val(order);
		$("#single").val(single);
		$("#multiple").val(multiple);
		$("#qanda").val(qanda);
		document.getElementById("submit_qn").click();
	}
}
addLoadEvent(onSubmitQuestionnaire);


/********************************for fetching the questionnaire *************************/
function prepareFetchQuestionnaire(){
	var list = document.getElementsByClassName("questionnaire-list");
	for(var i = 0; i < list.length; ++ i){
		list[i].pos = i;
		list[i].onclick = function(){
			$("#fetchingQuestionnaire").attr("action", "fetchingQuestionnaire.jsp?pos=" + this.pos);
			$("#fetch-questionnaire").click();
		}
	}
}
addLoadEvent(prepareFetchQuestionnaire);

/**************************This is for the search page *********************/
function prepareSearchQuestionnaire(){
	var list = document.getElementsByClassName("questionnaire-search-list");
	for(var i = 0; i < list.length; ++ i){
		list[i].pos = i;
		list[i].onclick = function(){
			$("#fetchingQuestionnaire").attr("action", "/VoteAndVoice/GetQuestionnaireResult?pos=" + this.pos);
			
			$("#fetch-questionnaire").click();
		}
	}
}
addLoadEvent(prepareSearchQuestionnaire);
function prepareSearchQuestion(){
	var list = document.getElementsByClassName("question-search-list");
	for(var i = 0; i < list.length; ++ i){
		list[i].pos = i;
		list[i].onclick = function(){
			$("#fetchingQuestion").attr("action", "/VoteAndVoice/GetQuestionResult?pos=" + this.pos);
			
			$("#fetch-question").click();
		}
	}
}
addLoadEvent(prepareSearchQuestion);







//
/*var showWeatherOnIndex_getImgClass = function(str){
    img_class = str.split('.')[0];
    if(img_class.length == 2){
        img_class = img_class[0] + "0" + img_class[1];
    }
    return "jpg80 " + img_class;
}
var showWeatherOnIndex = function(str){
    var json = JSON.parse(str);
    var weatherinfo = json.weatherinfo;
    var img1 = weatherinfo.img1;
    var img2 = weatherinfo.img2;
    img1_class = showWeatherOnIndex_getImgClass(img1);
    img2_class = showWeatherOnIndex_getImgClass(img2);
    var weather_img1 = document.getElementById("weather_img1");
    var weather_img2 = document.getElementById("weather_img2");
    weather_img1.setAttribute("class", img1_class);
    weather_img2.setAttribute("class", img2_class);
}*/

var mysleep = function(ms){
    start = new Date().getTime();
    while(true){
        if ((new Date().getTime()) - start > ms) {
            break;
        }
    }
}

var getUrlPrefix = function (url) {
    var temp = url.split("//");
    return temp[0] + "//" + temp[1].split("/")[0];
}

var extendNumber = function(number){
    return number < 10 ? "0" + parseInt(number) : number;
}
var showWeatherOnIndex_getImgClass = function (str) {
    if (str == '小雨') return "xiaoyu";
    if (str == '中雨') return "zhongyu";
    if (str == '大雨') return "dayu";
    if (str == '晴') return "qing";
    if (str == '多云') return "duoyun";
    if (str == '阴') return "yin";
    if (str.indexOf("雨") > 0) return "dayu";
    if (str.indexOf("阴") > 0) return "yin";
    if (str.indexOf("云") > 0) return "duoyun";
    return "qing";
}
var showWeatherOnIndex = function (str) {
    var strList = str.split(/\s+/g);
    var weather_div = document.getElementById("weather_div");
    if (str.indexOf("NetworkError") >= 0) {
        weather_div.innerHTML = '\
<i class="icon-qing">没有网络</i>\
<p>无法连接到网络，请重新尝试</p>';
        return;
    }
    city = strList[0];
    date = strList[1];
    weather = strList[3];
    temperature = strList[4];
    ing_class = showWeatherOnIndex_getImgClass(weather);
    weather_div.innerHTML = '\
<i class="icon-' + ing_class + '">' + temperature + '</i>\
<p>'
    + city +
    '<span>|</span>'
    + weather +
    '<span>|</span>'
    + date + '更新' +
'</p>'
}
var showTimeOnIndex = function () {
    var today = new Date();
    var year = today.getFullYear();
    var month = today.getMonth() + 1;
    var day = today.getDate();
    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    month = month < 10 ? "0" + month : month;
    day = day < 10 ? "0" + day : day;
    hours = hours < 10 ? "0" + hours : hours;
    minutes = minutes < 10 ? "0" + minutes : minutes;
    seconds = seconds < 10 ? "0" + seconds : seconds;

    var str = '<p>' + year + "年" + month + "月" + day + "日</p>\
<p>" + hours + "<span>:</span>" + minutes + "<span>:</span>" + seconds + "</p>";

    var obj = document.getElementById("time_div");
    obj.innerHTML = str;
    window.setTimeout("showTimeOnIndex()", 1000);
}

var transDomStrToValidate = function (str) {
    var result = "";
    for (var i = 0; i != str.length; i++) {
        var char = str.charAt(i);
        if (char == '!') {
            result += "!!";
        }
        else if (char == '<') {
            result += "!1";
        }
        else if (char == '>') {
            result += "!2";
        }
        else {
            result += char;
        }
    }
    return result;
}

var transValidateToDomStr = function (str) {
    var result = "";
    for (var i = 0; i != str.length; i++) {
        var char = str.charAt(i);
        if (char != '!') {
            result += char;
        }
        else {
            i++;
            char = str.charAt(i);
            if (char == '!') {
                result += "!";
            }
            else if (char == '1') {
                result += "<";
            }
            else if (char == '2') {
                result += ">";
            }
        }
    }
    return result;
}

var changeThisValue = function (e,str) {
    e.setAttribute("value", str);
}
var imgThisErrorSrc = function (e, str) {
    e.setAttribute("onerror", null);
    e.setAttribute("src", str);
}
var changeThisMarkBtn = function (e, handleUrl, u_id, e_id) {
    var operate;
    if (e.getAttribute("name") == "mark_btn_marked") {
        console.log("1");
        e.setAttribute("name", "mark_btn_unmarked");
        e.setAttribute("class", "btn btn-warning");
        e.setAttribute("value", "已标记");
        e.setAttribute("onmouseover", "changeThisValue(this,'取消标记');");
        e.setAttribute("onmouseout", "changeThisValue(this,'已标记');");
        operate = "mark";
    }
    else if (e.getAttribute("name") == "mark_btn_unmarked") {
        console.log("2");
        e.setAttribute("name", "mark_btn_marked");
        e.setAttribute("class", "btn btn-success");
        e.setAttribute("value", "标记");
        e.setAttribute("onmouseover", "changeThisValue(this,'点击标记');");
        e.setAttribute("onmouseout", "changeThisValue(this,'标记');");
        operate = "unmark";
    }
    $.post(
            handleUrl,
            {
                operate: operate,
                u_id: u_id,
                e_id: e_id
            },
            function () { });
}
var showOwnerAndMarkOnEvent = function (marked, o_id, o_name, u_id, e_id) {
    var obj = document.getElementById("owner_mark_div");
    if (marked == "self") {
        obj.innerHTML = '<p>本活动由我自己发布</p>\
                        <input class="btn btn-warning" value="编辑"\
                            onclick="window.location.href = window.location.href.replace(\'/Event?\', \'/EditEvent?\');"/>';
    }
    else {
        obj.innerHTML = '<img src="/img/user/' + o_id + '_.png" onerror="imgThisErrorSrc(this,\'/img/default/default-user-0.png\')" class="my-img-round" alt="(。・`ω´・)" width="32" height="32" />'
                    + '<p onclick="goToUserHomeByUId(\'' + o_id + '\')">发布者: ' + o_name + '#' + o_id + '</p>';
        if (marked == "false") {
            obj.innerHTML += '<input name="mark_btn_marked" class="btn btn-success" value="标记"\
id="mark_btn" onclick="changeThisMarkBtn(this,handleUrl,\'' + u_id + '\',\'' + e_id + '\');"\
onmouseover="changeThisValue(this,\'点击标记\');" onmouseout="changeThisValue(this,\'标记\');" />';
        }
        else if(marked == "true") {
            obj.innerHTML += '<input name="mark_btn_unmarked" class="btn btn-warning" value="已标记"\
id="mark_btn" onclick="changeThisMarkBtn(this,handleUrl,\'' + u_id + '\',\'' + e_id + '\');"\
onmouseover="changeThisValue(this,\'取消标记\');" onmouseout="changeThisValue(this,\'已标记\');" />';
        }
    }
}

/////////////////// New Event ///////////////////
var changeThisEventAuthBtn = function (e) {
    var operate;
    if (e.getAttribute("name") == "auth_btn_pub") {
        e.setAttribute("name", "auth_btn_pri");
        e.setAttribute("class", "btn btn-warning");
        e.setAttribute("value", "私有访问");
        e.setAttribute("onmouseover", "changeThisValue(this,'设为公开');");
        e.setAttribute("onmouseout", "changeThisValue(this,'私有访问');");
    }
    else if (e.getAttribute("name") == "auth_btn_pri") {
        e.setAttribute("name", "auth_btn_pub");
        e.setAttribute("class", "btn btn-success");
        e.setAttribute("value", "公开发布");
        e.setAttribute("onmouseover", "changeThisValue(this,'设为私有');");
        e.setAttribute("onmouseout", "changeThisValue(this,'公开发布');");
    }
}

var addActionSubmitDiv = function () {
    addActionSubmitDivFull(default_date, default_time, default_time, "");
}
var addActionSubmitDivFull = function(date,starttime,endtime,title) {
    time_list_div = document.getElementById("time_list_div");
    new_action_submit = document.createElement("div");
    new_action_submit.setAttribute("class", "row action-submit-div");
    new_action_submit.innerHTML = '\
                        <div class="row">\
                            <div class="col-md-4">\
                                <div class="laydate">\
                                    <input placeholder="请输入日期" class="laydate-icon form-control" value="' + date + '" onclick="laydate({format: \'YYYY-MM-DD\'})">\
                                </div>\
                            </div>\
                            <div class="col-md-4">\
                                <label class="control-label col-md-5">开始:</label>\
                                <div class="col-md-7">\
                                    <div class="input-group clockpicker-init">\
                                        <input type="text" class="form-control clockpicker-input-starttime" value="' + starttime + '" />\
                                        <span class="input-group-addon">\
                                            <span class="glyphicon glyphicon-time"></span>\
                                        </span>\
                                    </div>\
                                </div>\
                            </div>\
                            <div class="col-md-4">\
                                <label class="control-label col-md-5">结束:</label>\
                                <div class="col-md-7">\
                                    <div class="input-group clockpicker-init">\
                                        <input type="text" class="form-control clockpicker-input-endtime" value="' + endtime + '" />\
                                        <span class="input-group-addon">\
                                            <span class="glyphicon glyphicon-time"></span>\
                                        </span>\
                                    </div>\
                                </div>\
                            </div>\
                        </div>\
                        <div class="row">\
                            <div class="row col-md-10">\
                                <label class="control-label col-md-3">提醒标签:</label>\
                                <div class="col-md-9">\
                                    <input placeholder="请输入标签" class="form-control" value="">\
                                </div>\
                            </div>\
                            <div class="col-md-2">\
                                <input class="btn btn-danger" type="button" value="删除" onclick="deleteActionSubmitDiv(this);" />\
                            </div>\
                        </div>';
    if (title != null) {
        new_action_submit.firstElementChild.nextElementSibling.firstElementChild.firstElementChild.nextElementSibling.firstElementChild.setAttribute("value", title);
    }
    time_list_div.appendChild(new_action_submit);
    $('.clockpicker-init').clockpicker({
        autoclose: true
    }).find('input').change(function () {
        startList = $(".clockpicker-input-starttime");
        endList = $(".clockpicker-input-endtime");
        for (var i = 0; i != startList.size() ; i++) {
            if (startList[i].value > endList[i].value) {
                temp = startList[i].value;
                startList[i].value = endList[i].value;
                endList[i].value = temp;
            }
        }
    });
    $('.clockpicker-init').attr("class", "input-group clockpicker");
}

var deleteActionSubmitDiv = function (e) {
    var action_submit_div = e.parentElement.parentElement.parentElement;
    var action_submit_list = action_submit_div.parentElement;
    action_submit_list.removeChild(action_submit_div);
    if (action_submit_list.firstElementChild == null) {
        addActionSubmitDiv();
    }
}

var addLinkSubmitDiv = function () {
    addLinkSubmitDivFull("", "");
}
var addLinkSubmitDivFull = function (title, src) {
    link_list_div = document.getElementById("link_list_div");
    new_link_submit = document.createElement("div");
    new_link_submit.setAttribute("class", "row link-submit-div");
    new_link_submit.innerHTML = '\
                            <div class="row">\
                                <div class="col-sm-2">\
                                    <span class="glyphicon glyphicon-record"></span>\
                                </div>\
                                <div class="col-sm-8">\
                                    <input placeholder="链接名称" class="form-control" value="' + title + '">\
                                </div>\
                                <div class="col-sm-2">\
                                    <span class="glyphicon glyphicon-remove" onclick="deleteLinkSubmitDiv(this)"></span>\
                                </div>\
                            </div>\
                            <div class="row">\
                                <div class="col-sm-2">\
                                    <span class="glyphicon glyphicon-share-alt"></span>\
                                </div>\
                                <div class="col-sm-10">\
                                    <input placeholder="链接地址" class="form-control" value="' + src +'">\
                                </div>\
                            </div>';
    if (title != null) {
        new_link_submit.firstElementChild.firstElementChild.nextElementSibling.firstElementChild.setAttribute("value", title);
    }
    link_list_div.appendChild(new_link_submit);
}

var deleteLinkSubmitDiv = function (e) {
    var link_submit_div = e.parentElement.parentElement.parentElement;
    var link_submit_list = link_submit_div.parentElement;
    link_submit_list.removeChild(link_submit_div);
}

var addTagSubmitDiv = function () {
    addTagSubmitDivFull("");
}
var addTagSubmitDivFull = function (title) {
    link_list_div = document.getElementById("tag_list_div");
    new_tag_submit = document.createElement("div");
    new_tag_submit.setAttribute("class", "row link-submit-div");
    new_tag_submit.innerHTML = '\
                            <div class="row">\
                                <div class="col-sm-2">\
                                    <span class="glyphicon glyphicon-tag"></span>\
                                </div>\
                                <div class="col-sm-8">\
                                    <input placeholder="标签名称" class="form-control" value="' + title + '">\
                                </div>\
                                <div class="col-sm-2">\
                                    <span class="glyphicon glyphicon-remove" onclick="deleteLinkSubmitDiv(this)"></span>\
                                </div>\
                            </div>';
    if (title != null) {
        new_tag_submit.firstElementChild.firstElementChild.nextElementSibling.firstElementChild.setAttribute("value", title);
    }
    link_list_div.appendChild(new_tag_submit);
}

var getPostJsonForNewEvent = function () {
    var time_list_div = document.getElementById("time_list_div");
    var link_list_div = document.getElementById("link_list_div");
    var tag_list_div = document.getElementById("tag_list_div");
    var event = {};
    var action_list = [];
    var link_list = [];
    var tag_list = [];
    for (var child = time_list_div.firstElementChild; child != null; child = child.nextElementSibling) {
        console.log(child.toString());
        var action = {};
        var simdiv = child.firstElementChild.firstElementChild;
        action["date"] = simdiv.firstElementChild.firstElementChild.value;
        simdiv = simdiv.nextElementSibling;
        action["starttime"] = simdiv.lastElementChild.firstElementChild.firstElementChild.value;
        simdiv = simdiv.nextElementSibling;
        action["endtime"] = simdiv.lastElementChild.firstElementChild.firstElementChild.value;
        simdiv = child.firstElementChild.nextElementSibling.firstElementChild
        action["title"] = simdiv.lastElementChild.firstElementChild.value;
        action_list.push(action);
    }
    for (var child = link_list_div.firstElementChild; child != null; child = child.nextElementSibling) {
        var link = {};
        var simdiv = child.firstElementChild;
        link["title"] = simdiv.firstElementChild.nextElementSibling.firstElementChild.value;
        simdiv = simdiv.nextElementSibling;
        link["src"] = simdiv.firstElementChild.nextElementSibling.firstElementChild.value;
        link_list.push(link);
    }
    for (var child = tag_list_div.firstElementChild; child != null; child = child.nextElementSibling) {
        var tag = {};
        var simdiv = child.firstElementChild;
        tag["title"] = simdiv.firstElementChild.nextElementSibling.firstElementChild.value;
        tag_list.push(tag);
    }
    event["id"] = e_id;
    event["title"] = document.getElementById("input_event_title").value;
    event["content"] = transDomStrToValidate(document.getElementById("input_event_content").value);
    console.log(event["content"]);
    event["actions"] = action_list;
    event["links"] = link_list;
    event["tags"] = tag_list;
    var auth_name = document.getElementById("input_event_auth").getAttribute("name");
    if (auth_name == "auth_btn_pub") {
        event["auth"] = "pub";
    }
    else {
        event["auth"] = "pri";
    }
    return JSON.stringify(event);
}

var create_event_btn_func = function (handleUrl) {
    console.log(getPostJsonForNewEvent());
    $.post(
    handleUrl,
    {
        operate: "create",
        json_input: getPostJsonForNewEvent()
    },
        function (data) {
            console.log(data);
            if (isNaN(data)) {
                alert("网络异常，请重新尝试");
                return;
            }
            window.location.href = getUrlPrefix(handleUrl) + "/EventCentre/SuccessPage?e_id=" + data;
            //window.location.href = window.location.href.replace("/NewEvent", "/SuccessPage?e_id=" + data);
            //window.location.href = window.location.href.replace("/EditEvent?", "/SuccessPage?");
        }
    )
}

var delete_event_btn_func = function (handleUrl, e_id) {
    console.log(getPostJsonForNewEvent());
    $.post(
    getUrlPrefix(handleUrl) + "/EventCentre/EditEventDelete",
    {
        operate: "delete",
        e_id: e_id,
    },
        function (data) {
            console.log(data);
            if (isNaN(data)) {
                alert("网络异常，请重新尝试");
                return;
            }
            alert("已成功删除活动:" + data);
            window.location.href = getUrlPrefix(handleUrl) + "/PersonalSchedule/Index";
        }
    )
}

function html_encode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&/g, "&gt;");
    s = s.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
    s = s.replace(/ /g, "&nbsp;");
    s = s.replace(/\'/g, "&#39;");
    s = s.replace(/\"/g, "&quot;");
    return s;
}

function html_decode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&gt;/g, "&");
    s = s.replace(/&lt;/g, "<");
    s = s.replace(/&gt;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    s = s.replace(/&#39;/g, "\'");
    s = s.replace(/&quot;/g, "\"");
    s = s.replace(/&#176;/g, "°");
    return s;
}

/////////////////// Event ///////////////////
var addActionShowDiv = function (action_list) {
    time_list_div = document.getElementById("time_list_div");
    for (var i = 0; i != action_list.length; i++) {
        var action = action_list[i];
        title = action.title;
        starttime = action.starttime.split(" ");
        endtime = action.endtime.split(" ");
        date = starttime[0];
        strst = starttime[1];
        stret = endtime[1];
        time = strst.substr(0, strst.length - 3) + "-" + stret.substr(0, stret.length - 3);
        var new_action_submit = document.createElement("div");
        new_action_submit.setAttribute("class", "row");
        new_action_submit.innerHTML = '\
                            <label class="control-label label-over-hidden col-md-3">\
                                <span class="glyphicon glyphicon-calendar"></span>'
                                    + date +
                                '</label>\
                            <label class="control-label label-over-hidden col-md-3">\
                                <span class="glyphicon glyphicon-time"></span>'
                                    + time +
                                '</label>\
                            <label class="control-label label-over-hidden col-md-6">\
                                <span class="glyphicon glyphicon-flag"></span>'
                                + title +
                            '</label>';
        time_list_div.appendChild(new_action_submit);
    }
}

var addLinkShowDiv = function (link_list) {
    link_list_div = document.getElementById("link_list_div");
    for (var i = 0; i != link_list.length; i++) {
        var link = link_list[i];
        title = link.title;
        type = link.type;
        src = link.src;
        if (type == "inn") {
            src = "/EventCentre/Event?e_id=" + src;
        }
        new_link_submit = document.createElement("div");
        new_link_submit.setAttribute("class", "row link-submit-div");
        new_link_submit.innerHTML = '\
                            <div class="col-sm-2">\
                                <span class="glyphicon glyphicon-record"></span>\
                            </div>\
                            <a class="link-submit-div-clickable" href="' + src + '">\
                                <label class="control-label label-over-hidden col-md-8">' + title + '</label>\
                            </a>\
                            <div class="col-sm-2">\
                                <span class="glyphicon glyphicon-new-window link-submit-div-clickable" onclick="window.open(\'' + src + '\')"></span>\
                            </div>';
        link_list_div.appendChild(new_link_submit);
    }
}

var addTagShowDiv = function (tag_list) {
    tag_list_div = document.getElementById("tag_list_div");
    for (var i = 0; i != tag_list.length; i++) {
        var tag = tag_list[i];
        title = tag.title;
        new_tag = document.createElement("div");
        new_tag.setAttribute("class", "row link-submit-div");
        new_tag.innerHTML = '\
                            <div class="col-sm-2">\
                                <span class="glyphicon glyphicon-tag"></span>\
                            </div>\
                            <label class="control-label label-over-hidden col-md-10">' + title + '</label>';
        tag_list_div.appendChild(new_tag);
    }
}

var showMainTextOnEvent = function (str) {
    console.log(str);
    str = html_decode(str);
    console.log(str);
    document.getElementById("main_show_div").innerHTML += str;
}

/////////////////// EditEvent ///////////////////
var addActionEditDiv = function (action_list) {
    time_list_div = document.getElementById("time_list_div");
    for (var i = 0; i != action_list.length; i++) {
        var action = action_list[i];
        title = action.title;
        starttime = action.starttime.split(" ");
        endtime = action.endtime.split(" ");
        date = starttime[0].split("/");
        startt = starttime[1].split(":");
        endt = endtime[1].split(":");
        strdate = extendNumber(date[0]) + "-" + extendNumber(date[1]) + "-" + extendNumber(date[2]);
        strst = extendNumber(startt[0]) + ":" + extendNumber(startt[1]);
        stret = extendNumber(endt[0]) + ":" + extendNumber(endt[1]);
        addActionSubmitDivFull(strdate, strst, stret, title);
    }
}

var addLinkEditDiv = function (link_list) {
    link_list_div = document.getElementById("link_list_div");
    for (var i = 0; i != link_list.length; i++) {
        var link = link_list[i];
        title = link.title;
        type = link.type;
        src = link.src;
        addLinkSubmitDivFull(title, src);
    }
}

var addTagEditDiv = function (tag_list) {
    tag_list_div = document.getElementById("tag_list_div");
    for (var i = 0; i != tag_list.length; i++) {
        var tag = tag_list[i];
        title = tag.title;
        addTagSubmitDivFull(title);
    }
}


/********************** Search *********************/
var searchEventList = [];
var eventPerPage = 5;
var maxEventPage = function () {
    result = Math.floor((searchEventList.length - 1) / eventPerPage) + 1;
    if(result == 0) result = 1;
    return result;
}
var getToListEvent = function (e) {
    var str = "\
                    <div class=\"row tolist-event\">\
                        <div class=\"row tolist-event-main\">\
                            <label class=\"col-md-4 control-label label-over-hidden\" onclick=\"goToEventByEId(\'" + e.e_id + "\');\">" + e.e_title + "</label>\
                            <div class=\"col-md-4\">\
                                <span class=\"glyphicon glyphicon-user\"></span>\
                                <label class=\"control-label label-over-hidden\" onclick=\"goToUserHomeByUId(\'" + e.u_id + "\');\">" + e.u_name + "#" + e.u_id + "</label>\
                            </div>\
                            <div class=\"col-md-4\">\
                                <span class=\"glyphicon glyphicon-time\"></span>\
                                <label class=\"control-label\">" + e.e_time + "</label>\
                            </div>\
                        </div>\
                        <div class=\"col-md-12 tolist-event-tags\">\
                            <span class=\"glyphicon glyphicon-tags\"></span>"
    var i = 0;
    e.tags.forEach(function (tag) {
        if (i < 3) {
            str += "<label class=\"tolist-event-tags-tag label-over-hidden\">" + tag + "</label>";
            i++;
        }
    });
    str += "\
                        </div>\
                    </div>";
    return $(str);

}
var goToUserHomeByUId = function (u_id) {
    window.location.href = getUrlPrefix(window.location.href) + "/Search/Home?u_id=" + u_id;
}
var goToEventByEId = function (e_id) {
    window.location.href = getUrlPrefix(window.location.href) + "/EventCentre/Event?e_id=" + e_id;
}
var fetchEvents = function () {
    var search_input = $("#search_input").val();
    if (search_input == "") {
        return;
    }
    $.post(
        getUrlPrefix(window.location.href) + "/Search/FetchEvents",
        {
            title: search_input
        },
        function (data) {
            eventJSON = data;
            searchEventList = JSON.parse(eventJSON);
            $("#sum_page").html("/" + String(maxEventPage()));
            $("#current_page").val(1);
            showEvents(1);
        }
        );
}
var showEvents = function (pageNo) {
    $("#event_list").html("");
    var i = (pageNo - 1) * eventPerPage;
    for (; i != pageNo * eventPerPage && i != searchEventList.length ; i++) {
        $("#event_list").append(getToListEvent(searchEventList[i]));
    }
}
var searchEventsGoTo = function () {
    current_page = $("#current_page").val();
    if (current_page < 1) {
        $("#current_page").val(1);
        current_page = 1;
    }
    else if (current_page > maxEventPage()) {
        $("#current_page").val(maxEventPage());
        current_page = maxEventPage();
    }
    showEvents(current_page);
}
var searchEventsPre = function () {
    $("#current_page").val(parseInt($("#current_page").val()) - 1);
    searchEventsGoTo();
}
var searchEventsNext = function () {
    $("#current_page").val(parseInt($("#current_page").val()) + 1);
    searchEventsGoTo();
}
var searchEventsFirst = function () {
    $("#current_page").val(1);
    searchEventsGoTo();
}
var searchEventsLast = function () {
    $("#current_page").val(maxEventPage());
    searchEventsGoTo();
}
