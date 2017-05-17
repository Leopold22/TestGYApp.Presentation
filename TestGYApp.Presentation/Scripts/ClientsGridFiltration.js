
//загружаем выборку для первой страницы
$(function () {
    GetClients(1);   
});


//Вызов фильтрации по изменению значений в фильтрах
    //Имя:
    $("[id*=FirstNameFilterTextBox]").live("keyup", function () {
    GetClients(parseInt(1));
    });

    //Фамилия:
    $("[id*=LastNameFilterTextBox]").live("keyup", function () {
    GetClients(parseInt(1));
    });

    //Отчество:
    $("[id*=PatronymicFilterTextBox]").live("keyup", function () {
        GetClients(parseInt(1));
    });

    //Телефон:
    $("[id*=PhoneFilterTextBox]").live("keyup", function () {
        GetClients(parseInt(1));
    });

    //Маркетинг инфо:
    $("[id*=MarketingInfoDropDownFilter]").live("change", function () {
        GetClients(parseInt(1));
    });

    //Возраст от:
    $("[id*=AgeFromFilterTextBox]").live("keyup", function () {
        GetClients(parseInt(1));
    });

    //Возраст до:
    $("[id*=AgeToFilterTextBox]").live("keyup", function () {
        GetClients(parseInt(1));
    });

    //Дата рождения от:
    $("[id*=BirthDateFromFilterTextBox]").live("change", function () { 
        GetClients(parseInt(1));
    });

    //Дата рождения  до:
    $("[id*=BirthDateToFilterTextBox]").live("change", function () {
        GetClients(parseInt(1));
    });


//валидация даты:
    function DateValidation(date) {
        //проверяем дату на соответствие маске dd.mm.yyyy:
        var isValidMask = date.match(/^(\d{2})\.(\d{2})\.(\d{4})$/);
        if (isValidMask === null)
            return false;
        //если маска корректная, проверяем валидность даты:
        var d = +isValidMask[1], m = +isValidMask[2], y = +isValidMask[3];
        var date = new Date(y, m - 1, d);
        if (date.getFullYear() === y && date.getMonth() === m - 1) {
            return true;
        }
        return false;
    }



//Вызов фильрации по переключению страниц
$(".Pager .page").live("click", function () {
    GetClients(parseInt($(this).attr('page')));
});



//Кнопка "Очистить все фильтры"
$("[id*=ClearFiltersButton]").live("click", function () {
    $("[id*=FirstNameFilterTextBox]").val('');
    $("[id*=LastNameFilterTextBox]").val('');
    $("[id*=PatronymicFilterTextBox]").val('');
    $("[id*=PhoneFilterTextBox]").val('');
    $("[id*=AgeFromFilterTextBox]").val('');
    $("[id*=AgeToFilterTextBox]").val('');
    $("[id*=BirthDateFromFilterTextBox]").val('');
    $("[id*=BirthDateToFilterTextBox]").val('');
    $("[id*=MarketingInfoDropDownFilter]").val('');
    GetClients(1);

});




//Получение значений из поля фильтра
    //Имя:
    function SearchTermName() {   
        return  jQuery.trim($("[id*=FirstNameFilterTextBox]").val().replace(/[^A-Za-zа-яёА-ЯЁ]/gi, ''));
    };

    //Фамилия:
    function SearchTermLastName() {
        return jQuery.trim($("[id*=LastNameFilterTextBox]").val().replace(/[^A-Za-zа-яёА-ЯЁ]/gi, ''));
    };

    //Отчество:
    function SearchTermPatronymic() {
        return jQuery.trim($("[id*=PatronymicFilterTextBox]").val().replace(/[^A-Za-zа-яёА-ЯЁ]/gi, ''));
    };

    //Телефон:
    function SearchTermPhone() {
        return jQuery.trim($("[id*=PhoneFilterTextBox]").val());
    };


    //Маркетинг инфо:
    function SearchTermMarketingInfo() { 
        return jQuery.trim($("[id*=MarketingInfoDropDownFilter]").val());
    };


    //Возраст от:
    function SearchTermAgeFrom() {
        return jQuery.trim($("[id*=AgeFromFilterTextBox]").val());
    };


    //Возраст до:
    function SearchTermAgeTo() {
        return jQuery.trim($("[id*=AgeToFilterTextBox]").val());
    };



    //Дата рождения от:
    function SearchTermBirthDateFrom() {   
        var filterVal = jQuery.trim($("[id*=BirthDateFromFilterTextBox]").val());
        var result = null;
        if (filterVal !== "" &&  DateValidation(filterVal)) {        
            result = filterVal;    
        }    
        return result;
    };


    //Дата рождения до:
    function SearchTermBirthDateTo() {


        var filterVal = jQuery.trim($("[id*=BirthDateToFilterTextBox]").val());
        var result = null;
        if (filterVal !== "" && DateValidation(filterVal)) {
            result = filterVal;
        }
        return result;

    };



//Вызываем серверный метод обновления грида, передавая значения фильтров
function GetClients(pageIndex) {
    $.ajax({
        type: "POST",
        url: "TestPage.aspx/GetClients",
        data:
        JSON.stringify({
            "searchTermName": SearchTermName()
            , "searchTermLastName": SearchTermLastName()
            ,"searchTermPatronymic": SearchTermPatronymic()
            , "searchTermPhone": SearchTermPhone()
            ,"searchTermMarketingInfo": SearchTermMarketingInfo()
            , "searchTermAgeFrom": SearchTermAgeFrom() 
            , "searchTermAgeTo": SearchTermAgeTo()
            , "searchTermBirthDateFrom": SearchTermBirthDateFrom()
            , "searchTermBirthDateTo": SearchTermBirthDateTo()
            , "pageIndex": pageIndex 
        }),



        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
            alert("failure");
        },
        error: function (response) {
          alert(response.d);
           alert("error");
        }
    });
}
var row;
function OnSuccess(response) {
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var clients = xml.find("Clients");
    if (row == null) {
        row = $("[id*=MyClientsGridView] tr:last-child").clone(true);
    }
    $("[id*=MyClientsGridView] tr").not($("[id*=MyClientsGridView] tr:first-child")).remove();
    if (clients.length > 0) {
        $.each(clients, function () {
            var customer = $(this);
            //Заполняем имя как ссылку на форму просмотра
            $("td", row).eq(0).html("<a href=\"/ClientDispForm.aspx?id=" + $(this).find("ID").text() + "\">" + $(this).find("cl_name").text() + "</a>");
            //Заполняем номер телефона
            $("td", row).eq(1).html($(this).find("Phone").text());

            //форматируем номер телефона
            $("td", row).eq(1).text(function (i, text) {
                return text.replace(/(\d{0})(\d{3})(\d{3})(\d{2})/, '$1($2) $3-$4-');
            });


            //Заполняем Дату рождения
            $("td", row).eq(2).html($(this).find("BirthDate").text() );

            //Заполняем Возраст
            $("td", row).eq(3).html($(this).find("Age").text());

            //Заполняем E-mail
            $("td", row).eq(4).html($(this).find("Email").text());

            //Заполняем Примечание
            $("td", row).eq(5).html($(this).find("Comment").text());

            //Заполняем Откуда узнал
            $("td", row).eq(6).html($(this).find("MarketingInfo").text());

                
            $("[id*=MyClientsGridView]").append(row);
            row = $("[id*=MyClientsGridView] tr:last-child").clone(true);
        });


        //отрисовка пейджера
        var pager = xml.find("Pager");
        $(".Pager").ASPSnippets_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: parseInt(pager.find("PageIndex").text()),
            PageSize: parseInt(pager.find("PageSize").text()),
            RecordCount: parseInt(pager.find("RecordCount").text())



        });
 
        $(".cl_name").each(function () {
            var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
            $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
           
        });

        
        $(".Pager").show();

    } else { //если ничего не найдено:
        var empty_row = row.clone(true);
        $("td:first-child", empty_row).attr("colspan", $("td", row).length);
        $("td:first-child", empty_row).attr("align", "center");
        $("td:first-child", empty_row).attr("width", "1170px");
        $("td:first-child", empty_row).width("1105px");
        $("td:first-child", empty_row).html("По заданным критериям ничего не найдено.");
        $("td", empty_row).not($("td:first-child", empty_row)).remove();
        $("[id*=MyClientsGridView]").append(empty_row);
       
        $(".Pager").hide(); 

    }
};


