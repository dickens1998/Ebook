(function ($) {

    $.fn.desktopNotify = function (options) {

        var settings = $.extend({
            title: "Notification",
            options: {
                body: "",
                icon: "",
                sound: '#',
                lang: 'pt-BR',

            }
        }, options);

        this.init = function () {
            var notify = this;
            if (!("Notification" in window)) {
                console.log("This browser does not support desktop notification");
            } else if (Notification.permission === "granted") {

                var notification = new Notification(settings.title, settings.options);

                notification.onclose = function () {
                    if (typeof settings.options.onClose == 'function') {
                        settings.options.onClose();
                    }
                };

                notification.onclick = function () {
                    window.focus();
                };

                notification.onerror = function () {
                    if (typeof settings.options.onError == 'function') {
                        settings.options.onError();
                    }
                };

            } else if (Notification.permission !== 'denied') {
                Notification.requestPermission(function (permission) {
                    if (permission === "granted") {
                        notify.init();
                        //sound.remove();
                    }
                    if (permission == "denied") {
                        console.log("您拒绝显示通知");
                    }

                });
            }

        };

        this.init();
        var sound = $('<audio id="chatAudio" src="' + settings.options.sound + '"></audio>');
        sound[0].play();
        return this;
    };

}(jQuery));


