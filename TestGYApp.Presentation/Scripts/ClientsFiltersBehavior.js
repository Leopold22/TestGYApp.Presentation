
//*********** ФИЛЬТР ПО ДИАПАЗОНУ ВОЗРАСТА *************\\

//при загрузке страницы скрываем span с двойным фильтром
$(function () { 
    $('span.AgeFilterSpan').hide();
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


$(function () {
    $('span.BirthDateFilterSpan').hide();
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
    if ($("[id*=BirthDateFromFilterTextBox]").val() == ''
        && $("[id*=BirthDateToFilterTextBox]").val() == ''
        && ($("[id*=BirthDateToFilterTextBox]").is(':focus') == false)
        && ($('span.BirthDateOuterFilterSpan:hover').length == 0)
        && ($("[id=ui-datepicker-div]").is(':hover') == false)
    ) {
        $('span.BirthDateFilterSpan').hide();
        $('span.BirthDateCoverFilterSpan').show();
    }
});


//!!! выставляем заново фокус в двойной фильтр при клике на неактивную область календаря
$("[id=ui-datepicker-div]").live("click", function () {
    $("[id*=BirthDateFromFilterTextBox]").focus();

    var ndate = $("[id*=BirthDateFromFilterTextBox]").val();
    var myDate = $.format.date(ndate, "yyyy-mm-dd hh:mm:ss");
  // var myDate = new Date(ndate).format.toString('yyyy-mm-dd hh:mm:ss');
  // myDate.format('yyyy-mm-dd hh:mm:ss');
  alert($.format.date(ndate, "yyyy-mm-dd hh:mm:ss"));

    //alert($("[id*=BirthDateFromFilterTextBox]").val((new Date()).toString('dd.M.yy')).toString());
});



//Кнопка "Очистить все фильтры"
$("[id*=ClearFiltersButton]").live("click", function () {


    $('span.BirthDateFilterSpan').hide();
    $('span.BirthDateCoverFilterSpan').show();

    $('span.AgeFilterSpan').hide();
    $('span.AgeCoverFilterSpan').show();


});
