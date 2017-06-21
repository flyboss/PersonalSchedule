// Call this from the developer console and you can control both instances
var calendars = {};
//$(document).ready(function () {
var executeMyClndr = function (eventArray) {
    console.info(
        'Welcome to the CLNDR demo. Click around on the calendars and' +
        'the console will log different events that fire.');

    // Assuming you've got the appropriate language files,
    // clndr will respect whatever moment's language is set to.
    // moment.locale('ru');

    eventArray.sort(function (a, b) {
        if (a.date == b.date) {
            return a.time > b.time
        }
        return a.date > b.date
    })
    // The order of the click handlers is predictable. Direct click action
    // callbacks come first: click, nextMonth, previousMonth, nextYear,
    // previousYear, nextInterval, previousInterval, or today. Then
    // onMonthChange (if the month changed), inIntervalChange if the interval
    // has changed, and finally onYearChange (if the year changed).
    //console.log($('#template-calendar').html());
    calendars.clndr1 = $('.cal1').clndr({
        events: eventArray,
        multiDayEvents: {
            singleDay: 'date',
            endDate: 'endDate',
            startDate: 'startDate'
        },
        template: " <div class=\"clndr-controls\">\
                    <div class=\"clndr-control-button\">\
                        <span class=\"clndr-previous-button\">&nbsp;上一月&nbsp;</span>\
                    </div>\
                    <div class=\"month\"><%= month %>-<%= year %></div>\
                    <div class=\"clndr-control-button rightalign\">\
                        <span class=\"clndr-next-button\">&nbsp;下一月&nbsp;</span>\
                    </div>\
                </div>\
                <table class=\"clndr-table\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">\
                    <thead>\
                        <tr class=\"header-days\">\
                            <% _.each(daysOfTheWeek, function(day) { %>\
                            <td class=\"header-day\"><%= day %></td>\
                            <% }); %>\
                        </tr>\
                    </thead>\
                    <tbody>\
                        <% var i = 0;\
    _.each(days, function(day) {\
        if(i%7 == 0) { %>\
        <tr>\
            <%\
        }\
        %>\
        <td class=\"<%= day.classes %>\" data-toggle=\"modal\" data-target=\"#modal-<%= day.date.format('YYYY-MM-DD') %>\" >\
            <span class=\"day-number\"><%= day.day %></span>\
        </td>\
        <%  if(i%7 == 6) { %>\
    </tr>\
    <%\
        }\
        i++;}); %>\
    </tbody>\
</table>\
<div class=\"clndr-today-button\">返回今日</div>\
<div class=\"event-listing\">\
    <% var temp_date='';\
    _.each(eventsThisMonth, function(event) {\
        if(String(event.date) != 'undefined') {\
            if(temp_date != String(event.date) && temp_date != '') {\
                %>\
                <!--start footer: 单日事件模态框 -->\
                <div class=\"modal-footer\">\
                    <button type=\"button\" class=\"btn btn-default modal-btn\" data-dismiss=\"modal\">关闭</button>\
                </div>\
            </div>\
    </div>\
</div></div></div>\
<!--end footer: 单日事件模态框 -->\
<%\
}\
            if(temp_date != String(event.date)) {\
                temp_date = String(event.date);\
                %>\
                <!--start header: 单日事件模态框 -->\
                <div class=\"modal fade\" id=\"modal-<%= String(event.date) %>\" tabindex=\"-1\" role=\"dialog\" aria-hidden=\"true\">\
                    <div class=\"modal-dialog\">\
                        <div class=\"modal-content\">\
                            <div class=\"modal-header\" style=\"background-color:lightskyblue\">\
                                <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">\
                                    &times;\
                </button>\
                <h3 class=\"modal-title\"><%= temp_date %>的节目单</h3>\
            </div>\
            <div class=\"modal-body\">\
                <div class=\"form-horizontal\" role=\"form\">\
                    <!--end header: 单日事件模态框 -->\
                    <% } %>\
                    <form>\
                        <div class=\"form-group\">\
                            <label class=\"col-sm-3 control-label\"><%= event.time %></label>\
                            <label class=\"col-sm-7 control-label label-over-hidden\"><%= event.title %></label>\
                            <input class=\"btn btn-primary col-sm-2\" value=\"前往\" onclick=\"view_item('<%= event.itemid %>');\" />\
                        </div>\
                    </form>\
                    <%\
                            }\
    }); %>\
    <!--start footer: 单日事件模态框 -->\
    <div class=\"modal-footer\">\
        <button type=\"button\" class=\"btn btn-default modal-btn\" data-dismiss=\"modal\">关闭</button>\
    </div>\
</div>\
</div>\
</div>\
</div>\
</div>\
<!--end footer: 单日事件模态框 -->\
</div>",
        clickEvents: {
            click: function (target) {
                console.log('Cal-1 clicked: ', target.date.format('YYYY-MM-DD'));
            },
            nextInterval: function () {
                console.log('Cal-1 next interval');
            },
            previousInterval: function () {
                console.log('Cal-1 previous interval');
            },
            onIntervalChange: function () {
                console.log('Cal-1 interval changed');
            }
        }
    });

    // Bind all clndrs to the left and right arrow keys
    $(document).keydown(function (e) {
        // Left arrow
        if (e.keyCode == 37) {
            calendars.clndr1.back();
        }

        // Right arrow
        if (e.keyCode == 39) {
            calendars.clndr1.forward();
        }
    });
}

var view_item = function (itemid) {
    window.location.href = '/EventCentre/Event?e_id=' + itemid;
}