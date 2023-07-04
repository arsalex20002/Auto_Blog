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


    
});