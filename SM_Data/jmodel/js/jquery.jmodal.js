/// <reference path="jquery.js"/>
/*
 * jmodal
 * version: 3.0 (05/13/2009)
 * @ jQuery v1.3.*
 *
 * Licensed under the GPL:
 *   http://gplv3.fsf.org
 *
 * Copyright 2008, 2009 Jericho [ thisnamemeansnothing[at]gmail.com ] 
 *  
*/
$.extend($.fn, {
    jmodal: function(setting) {
        var ps = $.fn.extend({
            data: {},
            marginTop: 0,
            buttonText: { ok: 'Ok', cancel: 'Cancel' },
            okEvent: function(e) { },
            width: 400,
            height: 'auto',
            fixed: true,
            title: 'JModal Dialog',
            content: 'This is a jquery plugin!',
            skinId: 'jmodal-main'
        }, setting);
        var allSel = $('select').hide(), doc = $(document);

        ps.docWidth = doc.width();
        ps.docHeight = doc.height();
        var cache, cacheKey = 'jericho_modal';

        if ($('#jmodal-overlay').length == 0) {
            $('<div id="jmodal-overlay" class="jmodal-overlay"/>\
                <div class="jmodal-main" id="jmodal-main" >\
                    <div class="jmodal-top">\
                        <div class="jmodal-top-left jmodal-png-fiexed">&nbsp;</div>\
                        <div class="jmodal-border-top jmodal-png-fiexed">&nbsp;</div>\
                        <div class="jmodal-top-right jmodal-png-fiexed">&nbsp;</div>\
                    </div>\
                    <div class="jmodal-middle">\
                        <div class="jmodal-border-left jmodal-png-fiexed">&nbsp;</div>\
                        <div class="jmodal-middle-content">\
                           <div class="jmodal-rightclose" >\
                           <div class="jmodal-close" id="divclose">X&nbsp;</div></div>\
                           <div class="jmodal-title" id="divtitle" />\
                            <div class="jmodal-content" id="jmodal-container-content" />\
                            <div class="jmodal-opts">\
                                <input type="button"/>&nbsp;&nbsp;<input type="button" />\
                            </div>\
                        </div>\
                        <div class="jmodal-border-right jmodal-png-fiexed">&nbsp;</div>\
                    </div>\
                    <div class="jmodal-bottom">\
                        <div class="jmodal-bottom-left jmodal-png-fiexed">&nbsp;</div>\
                        <div class="jmodal-border-bottom jmodal-png-fiexed">&nbsp;</div>\
                        <div class="jmodal-bottom-right jmodal-png-fiexed">&nbsp;</div>\
                    </div>\
                </div>').appendTo('body');
            //$(document.body).find('form:first-child') || $(document.body)
        }

        if (window[cacheKey] == undefined) {
            cache = {
                overlay: $('#jmodal-overlay'),
                modal: $('#jmodal-main'),
                divtitle: $('#divtitle'),
                divclose: $('#divclose'),
                body: $('#jmodal-container-content')
            };
            cache.buttons = cache.body.next().children();
            window[cacheKey] = cache;
        }
        cache = window[cacheKey];
        var args = {
            hide: function() {
                cache.modal.fadeOut();
                cache.overlay.hide();
            },
            isCancelling: false
        };

        if (!cache.overlay.is(':visible')) {
            cache.overlay.css({ 
                                opacity: .4 
                                });
//            cache.body.css({ 
//                               
//                                height: ps.height
//                                });                  
            cache.modal.attr('class', ps.skinId)
                        .css({
                            position: (ps.fixed ? 'fixed' : 'absolute'),
                            width: ps.width,
                            left: (ps.docWidth - ps.width) / 2,
                            top: (ps.marginTop + document.documentElement.scrollTop)
                        }).fadeIn();
        }
        cache.divtitle.html(ps.title);
        //OK BUTTON
        cache.buttons.eq(0)
            .val(ps.buttonText.ok)
                .unbind('click')
                    .click(function(e) {
                        allSel.show();
                        ps.okEvent(ps.data, args);
                        if (!args.isCancelling) {
                            args.hide();
                        }
                    })
        //CANCEL BUTTON
            .next()
                .val(ps.buttonText.cancel)
                    .one('click', function() { args.hide(); allSel.show(); } );
                    
           cache.divclose.one('click', function() { args.hide(); allSel.show(); } );
         
        
         
         
        if (typeof ps.content == 'string') {
            $('#jmodal-container-content').html(ps.content);
        }
        if (typeof ps.content == 'function') {
            ps.content(cache.body);
        }
    }
})