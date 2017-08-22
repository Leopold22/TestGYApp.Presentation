

var sortingOrder = "FullName asc";
//var allItemsChecked;
var clients; //переменная для хранения выборки в xml
var emptyGrid; //если грид пустой после фильтрации - ставим в да, иначе - нет



$(function () {
  //  $("[id*=CheckedItemsCollector]").hide();
    $("[id*=ServerReportButton]").hide();
    $("[id*=MyClientsGridView] tr th.CheckboxHeader").html("<input type=\"checkbox\" id=\"selectAllCheckBox\" class=\"selectAllCheckBox\" >"); //рисуем общий чекбокс
});


//загружаем выборку для первой страницы
$(function () {
    GetClientsPageGrid(parseInt(1), sortingOrder);

});



//РАБОТА С ЧЕКБОКСАМИ

    //объект с информацией о выбранных элементах
    var checkedItemsInfo = {
        generalCheckboxChecked: false,
        checkedItemsArray: [],
        uncheckedItemsArray: []
    };



    //массив id
    //var checkedItemsArray = [];
    //var uncheckedItemsArray = [];



    //при клике на общий чекбокс
    $("[id*=MyClientsGridView] tr th.CheckboxHeader .selectAllCheckBox").live("click", function () {
        checkedItemsInfo.checkedItemsArray.splice(0, checkedItemsInfo.checkedItemsArray.length);
        checkedItemsInfo.uncheckedItemsArray.splice(0, checkedItemsInfo.uncheckedItemsArray.length);
        $("[id*=UncheckedItemsCollector]").val(checkedItemsInfo.uncheckedItemsArray);
        $("[id*=CheckedItemsCollector]").val(checkedItemsInfo.checkedItemsArray);

     
        checkedItemsInfo.generalCheckboxChecked = $("[id*=MyClientsGridView] tr th.CheckboxHeader .selectAllCheckBox:checkbox:checked").length > 0;
    

        if (checkedItemsInfo.generalCheckboxChecked) 
        {
            $("[id*=MyClientsGridView] tr td.CheckboxField .selectItemCheckBox").attr("checked", true);
        }
        else {
            $("[id*=MyClientsGridView] tr td.CheckboxField .selectItemCheckBox").prop("checked", false);
        }


    });


    //при клике на чекбокс
    $("[id*=MyClientsGridView] tr td.CheckboxField .selectItemCheckBox").live("click", function () {

        var checkboxElementId = $(this).attr('id');
        var itemID = checkboxElementId.match(/\d+/);
        var elementAdress = "[id*=MyClientsGridView] tr td.CheckboxField [id=" + checkboxElementId + "]:checkbox:checked";
        var isChecked = $(elementAdress).length > 0;

        // если выставлен флаг в общем чекбоксе
        if (checkedItemsInfo.generalCheckboxChecked) {

            if (isChecked) {

                for (var i = 0; i < checkedItemsInfo.uncheckedItemsArray.length; i++) {
                    var arrayElement = checkedItemsInfo.uncheckedItemsArray[i];
                    if (arrayElement.toString() === itemID.toString()) {
                        checkedItemsInfo.uncheckedItemsArray.splice(i, 1);
                        break;
                    }
                }
                
            }
            else {

                var isDublicate;
                for (var i = 0; i < checkedItemsInfo.uncheckedItemsArray.length; i++) {
                    var arrayElement = checkedItemsInfo.uncheckedItemsArray[i];
                    if (arrayElement.toString() === itemID.toString()) {
                        isDublicate = true;
                        break;
                    }
                }

                if (!isDublicate) { checkedItemsInfo.uncheckedItemsArray.push(itemID); };
             
            }

            $("[id*=UncheckedItemsCollector]").val(checkedItemsInfo.uncheckedItemsArray);


        }
        else {

            if (isChecked) {

                var isDublicate;
                for (var i = 0; i < checkedItemsInfo.checkedItemsArray.length; i++) {
                    var arrayElement = checkedItemsInfo.checkedItemsArray[i];
                    if (arrayElement.toString() === itemID.toString()) {
                        isDublicate = true;
                        break;
                    }
                }

                if (!isDublicate) { checkedItemsInfo.checkedItemsArray.push(itemID); };

            }
            else {

                for (var i = 0; i < checkedItemsInfo.checkedItemsArray.length; i++) {
                    var arrayElement = checkedItemsInfo.checkedItemsArray[i];
                    if (arrayElement.toString() === itemID.toString()) {
                        checkedItemsInfo.checkedItemsArray.splice(i, 1);
                        break;
                    }
                }
            }

            $("[id*=CheckedItemsCollector]").val(checkedItemsInfo.checkedItemsArray);

        }

    }); 



//функции для сортировки
    function GetSortedHeaderClass(sortingOrder) {

        var sortedHeaderClass;

        if (sortingOrder == "FullName asc" || sortingOrder == "FullName desc") { sortedHeaderClass = "FullNameHeader" };
        if (sortingOrder == "Phone asc" || sortingOrder == "Phone desc") { sortedHeaderClass = "PhoneHeader" };
        if (sortingOrder == "BirthDateForSort asc" || sortingOrder == "BirthDateForSort desc") { sortedHeaderClass = "BirthDateHeader" };
        if (sortingOrder == "Age asc" || sortingOrder == "Age desc") { sortedHeaderClass = "AgeHeader" };
        if (sortingOrder == "Email asc" || sortingOrder == "Email desc") { sortedHeaderClass = "EmailHeader" };
        if (sortingOrder == "Comment asc" || sortingOrder == "Comment desc") { sortedHeaderClass = "CommentHeader" };
        if (sortingOrder == "MarketingInfo asc" || sortingOrder == "MarketingInfo desc") { sortedHeaderClass = "MarketingInfoHeader" };

        return sortedHeaderClass;

    }

    function GetColumnNameByClass(sortedHeaderClass) {

        var columnName;

        if (sortedHeaderClass == "FullNameHeader") { columnName = "Имя"};
        if (sortedHeaderClass == "PhoneHeader") { columnName = "Телефон" };
        if (sortedHeaderClass == "BirthDateHeader") { columnName = "Дата рождения" };
        if (sortedHeaderClass == "AgeHeader") { columnName = "Возраст" };
        if (sortedHeaderClass == "EmailHeader") { columnName = "E-mail" };
        if (sortedHeaderClass == "CommentHeader") { columnName = "Примечание" };
        if (sortedHeaderClass == "MarketingInfoHeader") { columnName = "Откуда узнал о клубе" };

        return columnName;

    }


//ФУНКЦИОНАЛЬНЫЕ КНОПКИ

    //Кнопка "Сформировать отчет"
    $("[id*=ClientReportButton]").live("click", function () {
        var test = $("[id*=CheckedItemsCollector]").val();
        if (emptyGrid = true || !(checkedItemsInfo.generalCheckboxChecked || checkedItemsInfo.checkedItemsArray !== null || checkedItemsInfo.uncheckedItemsArray !== null))


        {
            bootbox.alert(
                {
                    message: "Не выбрано ни одного элемента",
                    size: 'small'
                }

                );


            alert("START...");
            //  $("[id*=ServerReportButton]").click();


            BuildExcelReport();


        }

        else {

            alert("START...");
          //  $("[id*=ServerReportButton]").click();


            BuildExcelReport();



        }
        
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
        GetClientsPageGrid(parseInt(1), sortingOrder);

    });




/////////////form
    function autoLogIn(un, pw) {
        var form = document.createElement("form");
        //var element1 = document.createElement("input");
        //var element2 = document.createElement("input");

        form.method = "POST";
      //  form.action = "login.php";

        //element1.value = un;
        //element1.name = "un";
        //form.appendChild(element1);

        //element2.value = pw;
        //element2.name = "pw";
        //form.appendChild(element2);

        //document.body.appendChild(form);

        form.submit();
    }




//при открытии страницы рисуем значок сортировки вверх для столбца Имя
$(function () {
    var text = $("[id*=MyClientsGridView] tr th.FullNameHeader").val();
    $("[id*=MyClientsGridView] tr th.FullNameHeader").html("<span class=\"FullNameHeaderText\">Имя</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
});

//при открытии страницы оборачиваем в span нвзвания всех остальных столбцов

$(function () {
    var text = $("[id*=MyClientsGridView] tr th.PhoneHeader").val();
    $("[id*=MyClientsGridView] tr th.PhoneHeader").html("<span class=\"PhoneHeaderText\">Телефон</span>");
}); // Телефон
$(function () {
    var text = $("[id*=MyClientsGridView] tr th.BirthDateHeader").val();
    $("[id*=MyClientsGridView] tr th.BirthDateHeader").html("<span class=\"BirthDateHeaderText\">Дата рождения</span>");
}); // Дата рождения
$(function () {
    var text = $("[id*=MyClientsGridView] tr th.AgeHeader").val();
    $("[id*=MyClientsGridView] tr th.AgeHeader").html("<span class=\"AgeHeaderText\">Возраст</span>");
}); // Возраст
$(function () { 
    var text = $("[id*=MyClientsGridView] tr th.EmailHeader").val();
    $("[id*=MyClientsGridView] tr th.EmailHeader").html("<span class=\"EmailHeaderText\">E-mail</span>");
}); // E-mail
$(function () { 
    var text = $("[id*=MyClientsGridView] tr th.CommentHeader").val();
    $("[id*=MyClientsGridView] tr th.CommentHeader").html("<span class=\"CommentHeaderText\">Примечание</span>");
}); // Примечание
$(function () { 
    var text = $("[id*=MyClientsGridView] tr th.MarketingInfoHeader").val();
    $("[id*=MyClientsGridView] tr th.MarketingInfoHeader").html("<span class=\"MarketingInfoHeaderText\">Откуда узнал о клубе</span>");
}); // Откуда узнал о клубе

//подчеркиваем название столбца при наведении курсора 
$("[id*=MyClientsGridView] tr th.FullNameHeader .FullNameHeaderText").live("mouseenter", function () { 
    $("[id*=MyClientsGridView] tr th.FullNameHeader .FullNameHeaderText").css("text-decoration", "underline");
}); // Имя
$("[id*=MyClientsGridView] tr th.PhoneHeader .PhoneHeaderText").live("mouseenter", function () {
    $("[id*=MyClientsGridView] tr th.PhoneHeader .PhoneHeaderText").css("text-decoration", "underline");
}); // Телефон
$("[id*=MyClientsGridView] tr th.BirthDateHeader .BirthDateHeaderText").live("mouseenter", function () {
    $("[id*=MyClientsGridView] tr th.BirthDateHeader .BirthDateHeaderText").css("text-decoration", "underline");
}); // Дата рождения
$("[id*=MyClientsGridView] tr th.AgeHeader .AgeHeaderText").live("mouseenter", function () {
    $("[id*=MyClientsGridView] tr th.AgeHeader .AgeHeaderText").css("text-decoration", "underline");
}); // Возраст
$("[id*=MyClientsGridView] tr th.EmailHeader .EmailHeaderText").live("mouseenter", function () {
    $("[id*=MyClientsGridView] tr th.EmailHeader .EmailHeaderText").css("text-decoration", "underline");
}); //  E-mail
$("[id*=MyClientsGridView] tr th.CommentHeader .CommentHeaderText").live("mouseenter", function () {
    $("[id*=MyClientsGridView] tr th.CommentHeader .CommentHeaderText").css("text-decoration", "underline");
}); //  Примечание
$("[id*=MyClientsGridView] tr th.MarketingInfoHeader .MarketingInfoHeaderText").live("mouseenter", function () {
    $("[id*=MyClientsGridView] tr th.MarketingInfoHeader .MarketingInfoHeaderText").css("text-decoration", "underline");
}); //  Откуда узнал о клубе

//снимаем подчеркивание при отведении курсора 
$("[id*=MyClientsGridView] tr th.FullNameHeader .FullNameHeaderText").live("mouseout", function () {
    $("[id*=MyClientsGridView] tr th.FullNameHeader .FullNameHeaderText").css("text-decoration", "none");
}); // Имя
$("[id*=MyClientsGridView] tr th.PhoneHeader .PhoneHeaderText").live("mouseout", function () {
    $("[id*=MyClientsGridView] tr th.PhoneHeader .PhoneHeaderText").css("text-decoration", "none");
}); // Телефон
$("[id*=MyClientsGridView] tr th.BirthDateHeader .BirthDateHeaderText").live("mouseout", function () {
    $("[id*=MyClientsGridView] tr th.BirthDateHeader .BirthDateHeaderText").css("text-decoration", "none");
}); // Дата рождения
$("[id*=MyClientsGridView] tr th.AgeHeader .AgeHeaderText").live("mouseout", function () {
    $("[id*=MyClientsGridView] tr th.AgeHeader .AgeHeaderText").css("text-decoration", "none");
}); // Возраст
$("[id*=MyClientsGridView] tr th.EmailHeader .EmailHeaderText").live("mouseout", function () {
    $("[id*=MyClientsGridView] tr th.EmailHeader .EmailHeaderText").css("text-decoration", "none");
}); //  E-mail
$("[id*=MyClientsGridView] tr th.CommentHeader .CommentHeaderText").live("mouseout", function () {
    $("[id*=MyClientsGridView] tr th.CommentHeader .CommentHeaderText").css("text-decoration", "none");
}); //  Примечание
$("[id*=MyClientsGridView] tr th.MarketingInfoHeader .MarketingInfoHeaderText").live("mouseout", function () {
    $("[id*=MyClientsGridView] tr th.MarketingInfoHeader .MarketingInfoHeaderText").css("text-decoration", "none");
}); //  Откуда узнал о клубе


// по клику на заголовок столбца Имя
$("[id*=MyClientsGridView] tr th.FullNameHeader .FullNameHeaderText").live("click", function () {    
    var headerInnerHtml = ($("[id*=MyClientsGridView] tr th.FullNameHeader")).html();
    var sortedHeaderClass = GetSortedHeaderClass(sortingOrder);
    var elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.DescSortButton"; $(elementName).hide();
    elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.AscSortButton"; $(elementName).hide();
    if (headerInnerHtml.indexOf("AscSortButton") >= 0) {
        $("[id*=MyClientsGridView] tr th.FullNameHeader").html("<span class=\"FullNameHeaderText\">Имя</span>" + "<span class=\"DescSortButton\"> &#9660;</span>");
        sortingOrder = "FullName desc";
    }
    else {       
        $("[id*=MyClientsGridView] tr th.FullNameHeader").html("<span class=\"FullNameHeaderText\">Имя</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
        sortingOrder = "FullName asc";
    } 
    GetClientsPageGrid(parseInt(1), sortingOrder);
});


// по клику на заголовок столбца Телефон
$("[id*=MyClientsGridView] tr th.PhoneHeader .PhoneHeaderText").live("click", function () {
    var headerInnerHtml = ($("[id*=MyClientsGridView] tr th.PhoneHeader")).html();
    var sortedHeaderClass = GetSortedHeaderClass(sortingOrder);
    var elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.DescSortButton"; $(elementName).hide();
    elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.AscSortButton"; $(elementName).hide();
    if (headerInnerHtml.indexOf("AscSortButton") >= 0) {
        $("[id*=MyClientsGridView] tr th.PhoneHeader").html("<span class=\"PhoneHeaderText\">Телефон</span>" + "<span class=\"DescSortButton\"> &#9660;</span>");
        sortingOrder = "Phone desc";
    }
    else {  
        $("[id*=MyClientsGridView] tr th.PhoneHeader").html("<span class=\"PhoneHeaderText\">Телефон</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
        sortingOrder = "Phone asc";
    }

    GetClientsPageGrid(parseInt(1), sortingOrder);
});


// по клику на заголовок столбца Дата рождения
$("[id*=MyClientsGridView] tr th.BirthDateHeader .BirthDateHeaderText").live("click", function () {
    var headerInnerHtml = ($("[id*=MyClientsGridView] tr th.BirthDateHeader")).html();
    var sortedHeaderClass = GetSortedHeaderClass(sortingOrder);
    var elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.DescSortButton"; $(elementName).hide();
    elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.AscSortButton"; $(elementName).hide();
    if (headerInnerHtml.indexOf("AscSortButton") >= 0) {
        $("[id*=MyClientsGridView] tr th.BirthDateHeader").html("<span class=\"BirthDateHeaderText\">Дата рождения</span>" + "<span class=\"DescSortButton\"> &#9660;</span>");
        sortingOrder = "BirthDateForSort desc";
    }
    else {
        $("[id*=MyClientsGridView] tr th.BirthDateHeader").html("<span class=\"BirthDateHeaderText\">Дата рождения</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
        sortingOrder = "BirthDateForSort asc";
    }
    GetClientsPageGrid(parseInt(1), sortingOrder);
});


// по клику на заголовок столбца Возраст
$("[id*=MyClientsGridView] tr th.AgeHeader .AgeHeaderText").live("click", function () {
    var headerInnerHtml = ($("[id*=MyClientsGridView] tr th.AgeHeader")).html();
    var sortedHeaderClass = GetSortedHeaderClass(sortingOrder);
    var elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.DescSortButton"; $(elementName).hide();
    elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.AscSortButton"; $(elementName).hide();
    if (headerInnerHtml.indexOf("AscSortButton") >= 0) {
        $("[id*=MyClientsGridView] tr th.AgeHeader").html("<span class=\"AgeHeaderText\">Возраст</span>" + "<span class=\"DescSortButton\"> &#9660;</span>");
        sortingOrder = "Age desc";
    }
    else {
        $("[id*=MyClientsGridView] tr th.AgeHeader").html("<span class=\"AgeHeaderText\">Возраст</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
        sortingOrder = "Age asc";
    }
    GetClientsPageGrid(parseInt(1), sortingOrder);
});


// по клику на заголовок столбца E-mail
$("[id*=MyClientsGridView] tr th.EmailHeader .EmailHeaderText").live("click", function () {
    var headerInnerHtml = ($("[id*=MyClientsGridView] tr th.EmailHeader")).html();
    var sortedHeaderClass = GetSortedHeaderClass(sortingOrder);
    var elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.DescSortButton"; $(elementName).hide();
    elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.AscSortButton"; $(elementName).hide(); 
    if (headerInnerHtml.indexOf("AscSortButton") >= 0) {
        $("[id*=MyClientsGridView] tr th.EmailHeader").html("<span class=\"EmailHeaderText\">E-mail</span>" + "<span class=\"DescSortButton\"> &#9660;</span>");
        sortingOrder = "Email desc";
    }
    else {
        $("[id*=MyClientsGridView] tr th.EmailHeader").html("<span class=\"EmailHeaderText\">E-mail</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
        sortingOrder = "Email asc";
    }
    GetClientsPageGrid(parseInt(1), sortingOrder);
});


// по клику на заголовок столбца Примечание
$("[id*=MyClientsGridView] tr th.CommentHeader .CommentHeaderText").live("click", function () {
    var headerInnerHtml = ($("[id*=MyClientsGridView] tr th.CommentHeader")).html();
    var sortedHeaderClass = GetSortedHeaderClass(sortingOrder);
    var elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.DescSortButton"; $(elementName).hide();
    elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.AscSortButton"; $(elementName).hide();
    if (headerInnerHtml.indexOf("AscSortButton") >= 0) {
        $("[id*=MyClientsGridView] tr th.CommentHeader").html("<span class=\"CommentHeaderText\">Примечание</span>" + "<span class=\"DescSortButton\"> &#9660;</span>");
        sortingOrder = "Comment desc";
    }
    else {
        $("[id*=MyClientsGridView] tr th.CommentHeader").html("<span class=\"CommentHeaderText\">Примечание</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
        sortingOrder = "Comment asc";
    }
    GetClientsPageGrid(parseInt(1), sortingOrder);
});


// по клику на заголовок столбца Откуда узнал о клубе
$("[id*=MyClientsGridView] tr th.MarketingInfoHeader .MarketingInfoHeaderText").live("click", function () {
    var headerInnerHtml = ($("[id*=MyClientsGridView] tr th.MarketingInfoHeader")).html();
    var sortedHeaderClass = GetSortedHeaderClass(sortingOrder);
    var elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.DescSortButton"; $(elementName).hide();
    elementName = "[id*=MyClientsGridView] tr th." + sortedHeaderClass + " span.AscSortButton"; $(elementName).hide();
    if (headerInnerHtml.indexOf("AscSortButton") >= 0) {
        $("[id*=MyClientsGridView] tr th.MarketingInfoHeader").html("<span class=\"MarketingInfoHeaderText\">Откуда узнал о клубе</span>" + "<span class=\"DescSortButton\"> &#9660;</span>");
        sortingOrder = "MarketingInfo desc";
    }
    else {
        $("[id*=MyClientsGridView] tr th.MarketingInfoHeader").html("<span class=\"MarketingInfoHeaderText\">Откуда узнал о клубе</span>" + "<span class=\"AscSortButton\"> &#9650;</span>");
        sortingOrder = "MarketingInfo asc";
    }
    GetClientsPageGrid(parseInt(1), sortingOrder);
});


//Вызов фильтрации по изменению значений в фильтрах
    //Имя:
    $("[id*=FirstNameFilterTextBox]").live("keyup", function () {
       // GetClients(parseInt(1), sortingOrder, false);
        GetClientsPageGrid(parseInt(1), sortingOrder); 

    });

    //Фамилия:
    $("[id*=LastNameFilterTextBox]").live("keyup", function () {
        GetClientsPageGrid(parseInt(1), sortingOrder);
    });

    //Отчество:
    $("[id*=PatronymicFilterTextBox]").live("keyup", function () {
        GetClientsPageGrid(parseInt(1), sortingOrder);
    });

    //Телефон:
    $("[id*=PhoneFilterTextBox]").live("keyup", function () {
        GetClientsPageGrid(parseInt(1), sortingOrder);
    });

    //Маркетинг инфо:
    $("[id*=MarketingInfoDropDownFilter]").live("change", function () {
        GetClientsPageGrid(parseInt(1), sortingOrder);
    });

    //Возраст от:
    $("[id*=AgeFromFilterTextBox]").live("keyup", function () {
        GetClientsPageGrid(parseInt(1), sortingOrder);
    });

    //Возраст до:
    $("[id*=AgeToFilterTextBox]").live("keyup", function () {
        GetClientsPageGrid(parseInt(1), sortingOrder);
    });

    //Дата рождения от:
    $("[id*=BirthDateFromFilterTextBox]").live("change", function () { 
        GetClientsPageGrid(parseInt(1), sortingOrder);
    });

    //Дата рождения  до:
    $("[id*=BirthDateToFilterTextBox]").live("change", function () {
        GetClientsPageGrid(parseInt(1), sortingOrder);
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
   // GetClients(parseInt($(this).attr('page')), sortingOrder, false);
    GetClientsPageGrid(parseInt($(this).attr('page')), sortingOrder);    
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



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////   Универсализация (эксперимент)  ////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




//Вызываем серверный метод обновления грида, передавая значения фильтров

function GetClientsPageGrid(pageIndex, sortOrder) {

    //Объект со значениями фильтров
    var myFilters = {
        sortOrder: sortOrder,
        searchTermName: SearchTermName(),
        searchTermLastName: SearchTermLastName(),
        searchTermPatronymic: SearchTermPatronymic(),
        searchTermPhone: SearchTermPhone(),
        searchTermMarketingInfo: SearchTermMarketingInfo(),
        searchTermAgeFrom: SearchTermAgeFrom(),
        searchTermAgeTo: SearchTermAgeTo(),
        searchTermBirthDateFrom: SearchTermBirthDateFrom(),
        searchTermBirthDateTo: SearchTermBirthDateTo(),
        pageIndex: pageIndex
    };

    var filters = JSON.stringify(filters)



    $.ajax({
        type: "POST",
        url: "TestPage.aspx/GetClientsPageGrid",
        data: JSON.stringify({ filters: myFilters }),
        //JSON.stringify({
        //    "sortOrder": sortOrder
        //    , "searchTermName": SearchTermName()
        //    , "searchTermLastName": SearchTermLastName()
        //    , "searchTermPatronymic": SearchTermPatronymic()
        //    , "searchTermPhone": SearchTermPhone()
        //    , "searchTermMarketingInfo": SearchTermMarketingInfo()
        //    , "searchTermAgeFrom": SearchTermAgeFrom()
        //    , "searchTermAgeTo": SearchTermAgeTo()
        //    , "searchTermBirthDateFrom": SearchTermBirthDateFrom()
        //    , "searchTermBirthDateTo": SearchTermBirthDateTo()
        //    , "pageIndex": pageIndex
        //}),

        //JSON.stringify(filters),
        
        




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
    // var myClients = xml.find("Clients");
    window.clients = xml.find("Clients");
    if (row == null) {
        row = $("[id*=MyClientsGridView] tr:last-child").clone(true);
    }
    $("[id*=MyClientsGridView] tr").not($("[id*=MyClientsGridView] tr:first-child")).remove();
    if (clients.length > 0) {
        emptyGrid = false; //грид не пустой
        $.each(clients, function () {
            var customer = $(this);

            //Заполняем колонку чекбокса
            $("td", row).eq(0).html("<input type=\"checkbox\" id=selectItemCheckBox" + $(this).find("ID").text() + "\ class=\"selectItemCheckBox\" >");

            //Заполняем имя как ссылку на форму просмотра
            $("td", row).eq(1).html("<a href=\"/ClientDispForm.aspx?id=" + $(this).find("ID").text() + "\">" + $(this).find("cl_name").text() + "</a>");
            //Заполняем номер телефона
            $("td", row).eq(2).html($(this).find("Phone").text());

            //форматируем номер телефона
            $("td", row).eq(2).text(function (i, text) {
                return text.replace(/(\d{0})(\d{3})(\d{3})(\d{2})/, '$1($2) $3-$4-');
            });

            //Заполняем Дату рождения
            $("td", row).eq(3).html($(this).find("BirthDate").text());

            //Заполняем Возраст
            $("td", row).eq(4).html($(this).find("Age").text());

            //Заполняем E-mail
            $("td", row).eq(5).html($(this).find("Email").text());

            //Заполняем Примечание
            $("td", row).eq(6).html($(this).find("Comment").text());

            //Заполняем Откуда узнал
            $("td", row).eq(7).html($(this).find("MarketingInfo").text());


            $("[id*=MyClientsGridView]").append(row);
            row = $("[id*=MyClientsGridView] tr:last-child").clone(true);
        });


        //генерируем пейджер
        var pager = xml.find("Pager");
        $(".Pager").ASPSnippets_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: parseInt(pager.find("PageIndex").text()),
            PageSize: parseInt(pager.find("PageSize").text()),
            RecordCount: parseInt(pager.find("RecordCount").text())



        });

        //подсветка совпадения текста со значением из фильтра
        //$(".cl_name").each(function () {
        //    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
        //    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));

        //});

        //показываем пейджер
        $(".Pager").show();


        //выставляем ранее отмеченные/снятые чекбоксы

        if (checkedItemsInfo.generalCheckboxChecked) {
            $("[id*=MyClientsGridView] tr td.CheckboxField .selectItemCheckBox").attr("checked", true);

            checkedItemsInfo.uncheckedItemsArray.forEach(function (item, i, checkedItemsArray) {
                var elementName = "[id=selectItemCheckBox" + item + "]";
                if ($(elementName.length)) {
                    $(elementName).attr("checked", false);
                }
            });

        }
        else {
            checkedItemsInfo.checkedItemsArray.forEach(function (item, i, checkedItemsArray) {
                var elementName = "[id=selectItemCheckBox" + item + "]";
                if ($(elementName.length)) {
                    $(elementName).attr("checked", true);
                }
            });
        }
       



    

    } else { //если ничего не найдено:

        emptyGrid = true; //грид не пустой
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

function BuildExcelReport() {

    //Объект со значениями фильтров
    var myFilters = {
        sortOrder: sortingOrder,
        searchTermName: SearchTermName(),
        searchTermLastName: SearchTermLastName(),
        searchTermPatronymic: SearchTermPatronymic(),
        searchTermPhone: SearchTermPhone(),
        searchTermMarketingInfo: SearchTermMarketingInfo(),
        searchTermAgeFrom: SearchTermAgeFrom(),
        searchTermAgeTo: SearchTermAgeTo(),
        searchTermBirthDateFrom: SearchTermBirthDateFrom(),
        searchTermBirthDateTo: SearchTermBirthDateTo(),
        pageIndex: 1
    };

    var filters = JSON.stringify(filters)

    var filters = JSON.stringify(filters)

    $.ajax({
        type: "POST",
        url: "TestPage.aspx/BuildExcelReport",
        data: JSON.stringify({
            generalCheckboxChecked: checkedItemsInfo.generalCheckboxChecked,
            checkedItemsArray: checkedItemsInfo.checkedItemsArray,
            uncheckedItemsArray: checkedItemsInfo.uncheckedItemsArray,
            filters: myFilters
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
       // success: OnSuccess,
        failure: function (response) {
            alert(response.d);
            alert("failure");
        },
        error: function (response) {
            alert(response.d);
            alert("error");
        }
    });




};