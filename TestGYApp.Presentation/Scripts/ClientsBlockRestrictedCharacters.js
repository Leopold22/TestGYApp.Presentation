
//Блокировка ввода запрещенных символов
//Имя: 
$("[id*=FirstNameFilterTextBox]").live("keypress", function () {
    var regex = new RegExp("^[a-zA-Zа-яёА-ЯЁ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }

});

//Фамилия: 
$("[id*=LastNameFilterTextBox]").live("keypress", function () {
    var regex = new RegExp("^[a-zA-Zа-яёА-ЯЁ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }

});

//Отчество: 
$("[id*=PatronymicFilterTextBox]").live("keypress", function () {
    var regex = new RegExp("^[a-zA-Zа-яёА-ЯЁ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }

});

//Телефон: 
$("[id*=PhoneFilterTextBox]").live("keypress", function () {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }

});

//Возраст от: 
$("[id*=AgeFromFilterTextBox]").live("keypress", function () {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }

});

//Возраст до: 
$("[id*=AgeToFilterTextBox]").live("keypress", function () {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }

});





//Блокировка копирования запрещенных символов
//Имя:
$("[id*=FirstNameFilterTextBox]").live("paste", function () {
    setTimeout(function () {
        //получаем значение поля
        var data = $("[id*=FirstNameFilterTextBox]").val();
        //заменяем запрещенные символы на '' 
        var dataFull = data.replace(/[^a-zA-Zа-яёА-ЯЁ]/gi, '');
        //результат вставляем в поле
        $("[id*=FirstNameFilterTextBox]").val(dataFull);
    });
});

//Фамилия:
$("[id*=LastNameFilterTextBox]").live("paste", function () {
    setTimeout(function () {
        //получаем значение поля
        var data = $("[id*=LastNameFilterTextBox]").val();
        //заменяем запрещенные символы на '' 
        var dataFull = data.replace(/[^a-zA-Zа-яёА-ЯЁ]/gi, '');
        //результат вставляем в поле
        $("[id*=LastNameFilterTextBox]").val(dataFull);
    });
});

//Отчество:
$("[id*=PatronymicFilterTextBox]").live("paste", function () {
    setTimeout(function () {
        //получаем значение поля
        var data = $("[id*=PatronymicFilterTextBox]").val();
        //заменяем запрещенные символы на '' 
        var dataFull = data.replace(/[^a-zA-Zа-яёА-ЯЁ]/gi, '');
        //результат вставляем в поле
        $("[id*=PatronymicFilterTextBox]").val(dataFull);
    });
});

//Телефон:
$("[id*=PhoneFilterTextBox]").live("paste", function () {
    setTimeout(function () {
        //получаем значение поля
        var data = $("[id*=PhoneFilterTextBox]").val();
        //заменяем запрещенные символы на '' 
        var dataFull = data.replace(/[^0-9]/gi, '');
        //результат вставляем в поле
        $("[id*=PhoneFilterTextBox]").val(dataFull);
    });
});


//Возраст от:
$("[id*=AgeFromFilterTextBox]").live("paste", function () {
    setTimeout(function () {
        //получаем значение поля
        var data = $("[id*=AgeFromFilterTextBox]").val();
        //заменяем запрещенные символы на '' 
        var dataFull = data.replace(/[^0-9]/gi, '');
        //результат вставляем в поле
        $("[id*=AgeFromFilterTextBox]").val(dataFull);
    });
});


//Возраст до:
$("[id*=AgeToFilterTextBox]").live("paste", function () {
    setTimeout(function () {
        //получаем значение поля
        var data = $("[id*=AgeToFilterTextBox]").val();
        //заменяем запрещенные символы на '' 
        var dataFull = data.replace(/[^0-9]/gi, '');
        //результат вставляем в поле
        $("[id*=AgeToFilterTextBox]").val(dataFull);
    });
});
