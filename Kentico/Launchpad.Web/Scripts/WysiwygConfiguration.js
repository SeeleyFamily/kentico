(function (pageBuilder) {
    var richTextEditor = pageBuilder.richTextEditor = pageBuilder.richTextEditor || {};
    var configurations = richTextEditor.configurations = richTextEditor.configurations || {};
    var plugins = richTextEditor.plugins = richTextEditor.plugins || [];

    var ctaPlugin = function (FroalaEditor) {
        FroalaEditor.DefineIcon('buttons', { NAME: 'cog', SVG_KEY: 'cogs' });
        FroalaEditor.RegisterCommand('buttons', {
            title: 'Add Button',
            type: 'dropdown',
            focus: true,
            undo: true,
            refreshAfterCallback: true,
            options: {
                'primary': 'Convert to Primary CTA',
                'secondary': 'Convert to Secondary CTA'
            },
            callback: function (cmd, val) {
                var currentHtml = this.html.getSelected();
                var selectedObj = $(currentHtml);
                //check that there is a link
                var link = $(selectedObj).find('a');
                if (link.length) {
                    $(selectedObj).find('a').removeClass().addClass(`btn btn-${val}`);
                    this.html.insert($(selectedObj).html());
                } else {
                    window.alert("No hyperlink selected. Please select a link to convert to a button")
                }
            }
        });
    }

    plugins.push(ctaPlugin);

    // Overrides the default configuration of widgets (i.e. when no configuration is specified)
    configurations["default"] = {
        charCounterCount: true,
        toolbarInline: false,
        toolbarSticky: false,
        // See SQL - SELECT DISTINCT(FileExtension) FROM Media_File;
        imageAllowedTypes: ['.mp3', '.svg', '.json', '.zip', '.wmv', '.jpg', '.wav', '.dot', '.gif', '.png', '.ics', '.eps', '.html', '.xlsx', '.xls', '.xlsb', '.psd', '.jpeg', '.pptx', '.css', '.ico', '.docx', '.jfif', '.swz', '.aspx', '.pdf', '.doc', '.ppt', '.swf', '.js', '.tif', '.txt', '.xlsm', '.htm','.mp4'],
        events: {
            'focus': function (clickEvent) {
                this.toolbar.show();
            },
            'blur': function (clickEvent) {
                this.toolbar.hide();
            },
            'initialized': function (clickEvent) {
                this.toolbar.hide();
            },
        },
        paragraphFormat: {
            N: 'Normal',
            H2: 'Heading 2',
            H3: 'Heading 3',
            H4: 'Heading 4',
            H5: 'Heading 5',
            H6: 'Heading 6',
        },
        paragraphStyles: {
            'intro-text': 'Intro Text',
        },
        toolbarButtons: {
            'moreText': {
                'buttons': ['bold',
                    'italic',
                    'underline',
                    'strikeThrough',
                    'subscript',
                    'superscript',
                    'inlineClass',
                    'inlineStyle',
                    'clearFormatting'
                ]
            },
            'moreParagraph': {
                'buttons': [
                    'formatOL',
                    'formatUL',
                    'formatOLSimple',
                    'paragraphFormat',
                    'paragraphStyle',
                    'outdent',
                    'indent',
                    'alignLeft',
                    'alignCenter',
                    'alignRight',
                    'alignJustify',
                    'quote'
                ],
                'buttonsVisible': 7
            },
            'moreRich': {
                'buttons': [
                    'buttons',
                    'insertLink',
                    'insertImage',
                    'insertVideo',
                    'insertTable',
                    'emoticons',
                    'specialCharacters',
                    'embedly',
                    'insertFile',
                    'insertHR'
                ]
            },
            'moreMisc': {
                'buttons': [
                    'undo',
                    'redo',
                    'fullscreen',
                    'print',
                    'getPDF',
                    'spellChecker',
                    'selectAll',
                    'html',
                    'help'
                ],
                'align': 'right',
                'buttonsVisible': 2
            }
        },
        // svg
        // see - https://github.com/froala/wysiwyg-editor/issues/2506
        htmlAllowedEmptyTags: ['textarea', 'a', 'iframe', 'object', 'video', 'style', 'script', '.fa', 'span', 'p', 'path', 'line'],
        htmlAllowedTags: ['.*'],
        htmlAllowedAttrs: ['.*'],
        htmlRemoveTags: ['script']
     };

    // Defines a new configuration for a simple toolbar with only formatting
    // options and disables the inline design of the toolbar
    configurations["simple"] = {
        toolbarButtons: ['paragraphFormat', '|', 'bold', 'italic', 'underline', '|', 'align', 'formatOL', 'formatUL'],
        paragraphFormatSelection: true,
        toolbarInline: false
        };
})(window.kentico.pageBuilder);