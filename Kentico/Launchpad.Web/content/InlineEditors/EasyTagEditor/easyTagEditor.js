(function () {
    window.kentico.pageBuilder.registerInlineEditor("easyTagEditor", {
        init: function (options) {
            var editor = options.editor;
            console.log(options);
            var delegate = $(editor).find(".element-delegate");
            if (delegate.length > 0) {
                var targetID = $(delegate).data("target-element");
                var targetElement = $("#" + targetID);
                $(targetElement).attr("contenteditable", true);
                $(targetElement).on("focusout", function () {
                    var event = new CustomEvent("updateProperty", {
                        detail: {
                            value: $(targetElement).text().trim(),
                            name: options.propertyName
                        }
                    });
                    editor.dispatchEvent(event);
                });
            }
        }
    });
})();