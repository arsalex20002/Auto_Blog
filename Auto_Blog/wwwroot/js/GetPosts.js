document.addEventListener("DOMContentLoaded", function () {
    var descriptionElements = document.querySelectorAll("[id^='description']");

    descriptionElements.forEach(function (element) {
        var descriptionText = element.textContent;

        if (descriptionText.length > 500) {
            var trimmedText = descriptionText.substring(0, 500);
            var lastPeriodIndex = trimmedText.lastIndexOf(".");

            if (lastPeriodIndex !== -1) {
                trimmedText = trimmedText.substring(0, lastPeriodIndex) + "...";
            } else {
                trimmedText += "...";
            }

            element.textContent = trimmedText;
        }
    });

    $(document).ready(function () {
        // Получение значения TypeCar из URL
        var urlParams = new URLSearchParams(window.location.search);
        var typeCar = urlParams.get('TypeCar');

        // Установка значения TypeCar в выпадающем списке
        $('#modelSelectType').val(TypeCar);
    });

    $(document).ready(function () {
        // Загрузка списка моделей машин
        $.ajax({
            url: '/Car/GetCarModels', // URL-адрес действия контроллера, возвращающего список моделей
            type: 'GET',
            success: function (models) {

                // Добавление каждой модели в <select> в виде опции
                $.each(models, function (index, model) {
                    $('#modelSelect').append($('<option></option>').val(model).text(model));
                });
            },
            error: function () {
                // Обработка ошибки при загрузке списка моделей
                console.log('Ошибка загрузки списка моделей машин');
            }
        });
    });
    
});