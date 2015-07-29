function settingsInit() {
    $("#settingSave").submit(function (event) {
        event.preventDefault();

        var data = "";
        $(this).find(":input[type=hidden].data").each(function () {
            if (data != "")
                data = data + ",";
            data = data + '"' + $(this).attr("name") + '":"' + $(this).val() + '"';
        });

        $(this).find(":input[type=text]").each(function () {
            if (data != "")
                data = data + ",";
            data = data + '"' + $(this).attr("name") + '":"' + $(this).val() + '"';
        });

        $(this).find("textarea").each(function () {
            if (data != "")
                data = data + ",";
            data = data + '"' + $(this).attr("name") + '":"' + $(this).val() + '"';
        });

        $(this).find(":input[type=checkbox]").each(function () {
            if (data != "")
                data = data + ",";
            data = data + '"' + $(this).attr("name") + '":' + $(this).is(':checked') + '';
        });

        Messenger.options = {
            extraClasses: 'messenger-fixed messenger-on-bottom messenger-on-right',
            theme: 'ice'
        }

        if (data != "") {
            $.ajax({
                type: "POST",
                data: "{" + data + "}",
                contentType: "application/json",
                success: function (data) {
                    Messenger.options = {
                        extraClasses: 'messenger-fixed messenger-on-bottom messenger-on-right',
                        theme: 'ice'
                    };

                    Messenger().post({
                        message: 'The settings were saved.',
                        showCloseButton: true,
                        type: "success"
                    });
                },
                error: function (err) {
                    Messenger.options = {
                        extraClasses: 'messenger-fixed messenger-on-bottom messenger-on-right',
                        theme: 'ice'
                    }
                    Messenger().post({
                        message: 'There was an error and your information was not saved.',
                        showCloseButton: true,
                        type: "error"
                    });
                }
            });
        } else {
            Messenger.options = {
                extraClasses: 'messenger-fixed messenger-on-bottom messenger-on-right',
                theme: 'ice'
            }
            Messenger().post({
                message: 'There was an error and your information was not saved.',
                showCloseButton: true,
                type: "error"
            });
        }

        return false;
    });
}