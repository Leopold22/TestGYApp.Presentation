
//*********** ФИЛЬТР ПО ДИАПАЗОНУ ВОЗРАСТА *************\\

//при загрузке страницы скрываем span с двойным фильтром
$(function () {

    if ($("[id*=AgeFromFilterTextBox]").val() == '' && $("[id*=AgeToFilterTextBox]").val() == '') {
        $('span.AgeFilterSpan').hide();
        $('span.AgeCoverFilterSpan').show();
    }
    else {
        $('span.AgeFilterSpan').show();
        $('span.AgeCoverFilterSpan').hide();
    }
});

//показываем двойной фильтр когда мышь внутри внешнего контейнера
$('span.AgeOuterFilterSpan').live("mouseenter", function () {
    $('span.AgeFilterSpan').show();
    $('span.AgeCoverFilterSpan').hide();
});

//скрываем двойной фильтр когда мышь уходит с внешнего контейнера
//если фильтры пустые и в них не стоит фокус
$('span.AgeOuterFilterSpan').live("mouseleave", function () {
    if (
        $("[id*=AgeFromFilterTextBox]").val() == ''
        && $("[id*=AgeToFilterTextBox]").val() == ''
        && ($("[id*=AgeFromFilterTextBox]").is(':focus') == false)
        && ($("[id*=AgeToFilterTextBox]").is(':focus') == false)
    ) {
        $('span.AgeFilterSpan').hide();
        $('span.AgeCoverFilterSpan').show();
    }
});

//скрываем двойной фильтр при клике в другую область если фильтры пустые
$('span.AgeOuterFilterSpan').live("blur", function () {   

    if ($("[id*=AgeFromFilterTextBox]").val() == ''
        && $("[id*=AgeToFilterTextBox]").val() == ''
        && ($("[id*=AgeToFilterTextBox]").is(':focus') == false)
        && ($('span.AgeOuterFilterSpan:hover').length == 0)
    ) {
        $('span.AgeFilterSpan').hide();
        $('span.AgeCoverFilterSpan').show();
    }
});




//*********** ФИЛЬТР ПО ДИАПАЗОНУ ДАТ РОЖДЕНИЯ *************\\

//при открытии страницы скрываем двойной фильтр + выставляем параметры дейтпикеров 
$(function () {

    $('span.BirthDateFilterSpan').hide();

    $("[id*=BirthDateFromFilterTextBox]").datepicker(
        {
           showOn: 'both',
            changeMonth: true,
            changeYear: true,
            buttonImage: "/Images/AddButton.png",
            buttonImageOnly: true,
         //   showButtonPanel: true
        }
    );


    $("[id*=BirthDateToFilterTextBox]").datepicker(
        {
            showOn: 'both',
            changeMonth: true,
            changeYear: true,
            buttonImage: "/Images/AddButton.png",
            buttonImageOnly: true,
            //   showButtonPanel: true
        }
    );



});



//показываем двойной фильтр когда мышь внутри внешнего контейнера
$('span.BirthDateOuterFilterSpan').live("mouseenter", function () {
    $('span.BirthDateFilterSpan').show();
    $('span.BirthDateCoverFilterSpan').hide();
});

//скрываем двойной фильтр когда мышь уходит с внешнего контейнера
//если фильтры пустые и в них не стоит фокус
$('span.BirthDateOuterFilterSpan').live("mouseleave", function () {
    if (
        $("[id*=BirthDateFromFilterTextBox]").val() == ''
        && $("[id*=BirthDateToFilterTextBox]").val() == ''
        && ($("[id*=BirthDateFromFilterTextBox]").is(':focus') == false)
        && ($("[id*=BirthDateToFilterTextBox]").is(':focus') == false)
    ) {
        $('span.BirthDateFilterSpan').hide();
        $('span.BirthDateCoverFilterSpan').show();
    }
});

//скрываем двойной фильтр при клике в другую область если фильтры пустые
$('span.BirthDateOuterFilterSpan').live("blur", function () {

    var BirthDateFrom = $("[id*=BirthDateFromFilterTextBox]").val();
    var BirthDateTo = $("[id*=BirthDateToFilterTextBox]").val();

    if (BirthDateFrom == ''
        && BirthDateTo == ''
        && ($("[id*=BirthDateFromFilterTextBox]").is(':focus') == false) //не было
        && ($("[id*=BirthDateToFilterTextBox]").is(':focus') == false)
        && ($('span.BirthDateOuterFilterSpan:hover').length == 0)
        && ($("[id=ui-datepicker-div]").is(':hover') == false)
    ) {
   
        $('span.BirthDateFilterSpan').hide();
        $('span.BirthDateCoverFilterSpan').show();
    }



    if (BirthDateFrom !== "" &&  !DateValidation(BirthDateFrom)) {
       // alert("BirthDateFrom not valid");
        $("[id*=BirthDateFromFilterTextBox]").val("");
       
    }
    if (BirthDateTo !== "" && !DateValidation(BirthDateTo)) {
       // alert("BirthDateTo not valid");
        $("[id*=BirthDateToFilterTextBox]").val("") ;
    }
});



//контролируем поведение при клике на неактивную область дейтпикера 
(function () {

   
    var prevFocus; //сюда записываем, в какое поле выставлен фокус

    $("[id*=BirthDateFromFilterTextBox]").live("click", function () {
        prevFocus = "datefrom";    
    });

    $("[id*=BirthDateToFilterTextBox]").live("click", function () {
        prevFocus = "dateto"; 
    });

    //при клике на неактивную область дейтпикера возвращаем фокус в то поле, откуда он был вызван
    //$("[id=ui-datepicker-div]").live("click", function () {
    $('.ui-datepicker-calendar').live('click', function () {
        //ui-datepicker-calendar

        //if ($("[.ui-datepicker-title]").is(':click') == true) {
        //    alert("omg!");
        //}
      //  alert("yeah baby");
       
        if (prevFocus == "datefrom")
        {
            $("[id*=BirthDateFromFilterTextBox]").focus();
        }

        if (prevFocus == "dateto") {
            $("[id*=BirthDateToFilterTextBox]").focus();
        }        

    });


})();






//Кнопка "Очистить все фильтры"
$("[id*=ClearFiltersButton]").live("click", function () {


    $('span.BirthDateFilterSpan').hide();
    $('span.BirthDateCoverFilterSpan').show();

    $('span.AgeFilterSpan').hide();
    $('span.AgeCoverFilterSpan').show();


});
