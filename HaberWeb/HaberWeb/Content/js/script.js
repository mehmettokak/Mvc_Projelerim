jQuery(function () {
			/* Tools */
	var bd = jQuery('body');
	var layout = jQuery.cookie('layout');
	if (layout && layout == 'fixed') {
		bd.addClass('fixed');
		jQuery('#tools a.layout span').removeClass('active');
		jQuery('#tools #fixed span').addClass('active');
	}
	if (jQuery.cookie('tools')==1) {
		bd.addClass('active_tools');
	}
	var color = jQuery.cookie('colors');
	if (color) {
		bd.removeClass('red blue orange green grey dark_blue dark_grey').addClass(color);
		jQuery('#tools_colors a').removeClass('active');
		jQuery('#tools_colors .' +color+ ' a').addClass('active');
	}
	jQuery('#tools_colors a').click(function() {
		if (jQuery(this).hasClass('active')) { return false; }
		jQuery('#tools_colors a').removeClass('active');
		jQuery(this).addClass('active');
		bd.removeClass('red blue orange green grey dark_blue dark_grey').addClass(jQuery(this).text());
		jQuery.cookie('colors', jQuery(this).text());
		return false;
	});
	jQuery('#block_secondary_menu li.color a').click(function() {
		jQuery('#block_secondary_menu li').removeClass('active');
		jQuery(this).parent().addClass('active');
		bd.removeClass('red blue orange green grey dark_blue dark_grey').addClass(jQuery(this).parent().data('class'));
		jQuery.cookie('colors', jQuery(this).parent().data('class'));
		return false;
	});
	jQuery('#tools h3').click(function() {
		if (bd.hasClass('active_tools')) {
			jQuery('#tools').animate({ 'left': '-119px'});
			jQuery.cookie('tools', 0);
			bd.removeClass('active_tools');
		} else {
			jQuery('#tools').animate({ 'left': '0'});
			jQuery.cookie('tools', 1);
			bd.addClass('active_tools');
		}
	});
	jQuery('#tools a.layout').click(function() {
		var $this = jQuery(this).find('span');
		if ($this.hasClass('active')) { return false; }
		var layout = $this.text();
		if (bd.hasClass('fixed')) {bd.removeClass('fixed');}
		if (layout == 'fixed') {bd.addClass('fixed');}
		jQuery('#tools a.layout span').removeClass('active');
		$this.addClass('active');
		jQuery.cookie('layout', layout);
		return false;
	});
	// Set Date in Header - this part only for Demo, in real life please create date on server
	var weekDay = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
	var monthName = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
	var today = new Date();
	var todayString = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
	jQuery('time.today, time.last_update').attr({'datetime': todayString});
	jQuery('time.today').empty().text(weekDay[today.getDay()] + ', ' + monthName[today.getMonth()] + ' ' + today.getDate() + ', ' + today.getFullYear());
	
	// override default jQuery FadeTo method for IE < 9
	if ((jQuery.browser.msie) && (parseInt(jQuery.browser.version) < 9)) {
		jQuery.fn.fadeTo = function( speed, to, easing, callback ) {return this.animate({}, speed, easing, callback);}
	}
	
	// Gallery Front Page
	jQuery('#block_front_slider .content').adGallery({loader_image: 'images/preloader.gif', width: 472, height: 332, scroll_jump: 1, display_next_and_prev: false, display_back_and_forward: false, 
	slideshow: { enable: true, autostart: true, speed: 10000} });
	// Gallery Page
	jQuery('#content .ad-gallery').adGallery({loader_image: 'images/preloader.gif', width: 618, height: 468, scroll_jump: 1, display_next_and_prev: true, display_back_and_forward: true});
	
	// Slider Breaking News
	jQuery('#block_breaking_news ul').anythingSlider({buildNavigation: false, hashTags: false, autoPlay: true, delay: 15000, vertical: true});
	
	// Slider Video
	// loading Vimeo thumbs
	jQuery('#block_video li.vimeo').each(function(index) {
			var _this = this;
			var url = jQuery(_this).find('a').attr('href');
			var video_id = url.match("vimeo\.com\/([0-9]{1,10})");
			video_id = video_id[1];
			jQuery.ajax({
				type:'GET',
				url: 'http://vimeo.com/api/v2/video/' + video_id + '.json',
				dataType: 'jsonp',
				success: function(data){
					var thumbnail_src = data[0].thumbnail_small;
					jQuery(_this).append('<div class="thumb"><div class="bg"/><img src="' + thumbnail_src + '" alt=""/></div>');
				}
			});
    });
	jQuery('#block_video ul').anythingSlider({buildStartStop: false, buildNavigation: false, hashTags: false, expand:true, showMultiple : 4, changeBy: 4}); // activate Slider
	jQuery('#block_video li.vimeo').click(function() {
		if (!jQuery(this).hasClass('selected')) {
			jQuery('#block_video li.vimeo').removeClass('selected').find('.bg').hide();
			jQuery(this).addClass('selected');
			var url = jQuery(this).find('a').attr('href');
			var video_id = url.match("vimeo\.com\/([0-9]{1,10})");
			video_id = video_id[1];
			jQuery('#block_video .video').empty().append('<iframe src="http://player.vimeo.com/video/' + video_id + '?title=0&amp;byline=0&amp;portrait=0" width="280" height="213"></iframe>');
			jQuery('#block_video li:not(.selected)').fadeTo('fast', 0.7);
		}
		return false;
	}); // add video on click
	jQuery('#block_video li.activePage').click();
	// add opacity on hover
	jQuery('#block_video li').hover(function() {
			jQuery(this).stop().fadeTo(300, 1).find('.bg').show();
	}, function() {
		if (!jQuery(this).hasClass('selected')) {
			jQuery(this).stop().fadeTo(300, 0.7).find('.bg').hide();
		}
	});
	
	// Slider Editors Choice
	jQuery('#block_editors_choice ul').anythingSlider({buildStartStop: false, expand:true, showMultiple : 3, changeBy: 3, appendNavigationTo: '#block_editors_choice', hashTags: false});
	jQuery('#block_editors_choice .thumbNav li:nth-child(3n+1)').show(); // show nav
	
	// Slider In Pictures
	jQuery('#block_in_pictures ul').anythingSlider({buildStartStop: false, appendNavigationTo: '#block_in_pictures', hashTags: false});
	jQuery('#block_in_pictures .item a').colorbox({rel: true, current: false, maxWidth: 800, maxHeight: 600, preloading: false, transition:'fade'}); // Popup images
	
	jQuery('#block_in_pictures .item a, #gallery_list .photo a').append('<span class="bg"/>');
	jQuery('#block_in_pictures .item a, #gallery_list .photo a').hover(function() { // show zoom bg
		jQuery(this).find('.bg').css({'display': 'block'});
	}, function() {
		jQuery(this).find('.bg').css({'display': 'none'});
	});
	
	// Gallery List
	jQuery('.gallery_4columns .gallery_without_description li:nth-child(4n), .gallery_4columns .gallery_with_description li:nth-child(2n), .gallery_3columns .gallery_without_description li:nth-child(3n)').addClass('last_gallery'); // remove padding in last item
	jQuery('.change_gallery a').click(function() {
		if (!jQuery(this).hasClass('active')) {
			if (jQuery(this).hasClass('with_description')) {
				var remove = 'gallery_without_description', add = 'gallery_with_description';
			} else {
				var remove = 'gallery_with_description', add = 'gallery_without_description';
			}
			jQuery('#gallery_list ul').fadeOut('fast', 
					function() {
						jQuery(this).removeClass(remove).addClass(add);
						jQuery('#gallery_list li').removeClass('last_gallery');
						jQuery('.gallery_4columns .gallery_without_description li:nth-child(4n), .gallery_4columns .gallery_with_description li:nth-child(2n), .gallery_3columns .gallery_without_description li:nth-child(3n)').addClass('last_gallery'); // remove padding in last item
						jQuery(this).fadeIn();
			});
			jQuery(this).addClass('active').siblings().removeClass('active');
		}
		return false;
	});
	
	// Tabs, cookie expires in days
	jQuery('#block_sidebar_tabs, #block_content_top_tabs, .tabs').tabs({ cookie:{ expires: 7 } });

	// Scroll window to Top
	jQuery('#scroll_to_top').click(function() {
        jQuery('body,html').animate({scrollTop: 0});
		return false;
    });
	
	// animate hover
	jQuery('.view_all:not(.active), .view_all_medium:not(.active), .view_all_big:not(.active), .anythingSlider .arrow, .anythingSlider .start-stop, input.form-submit, .button_plus, .button_minus, .button_next, .button_back, .change_gallery a, .email_print_pdf a').fadeTo('fast', 0.5);
	ChangeOpacity = function(obj) {
		jQuery(obj).fadeTo(400, 1);
		jQuery(obj).fadeTo(400, 0.5);
	};
	var IntervalId = 0;
	jQuery('.view_all:not(.active), .view_all_medium:not(.active), .view_all_big:not(.active), .anythingSlider .arrow, .anythingSlider .start-stop, input.form-submit, .button_plus, .button_minus, .button_next, .button_back, .change_gallery a, .email_print_pdf a').hover(function() {
		var _this = this;
		ChangeOpacity(_this);
		IntervalId = setInterval(function () {ChangeOpacity(_this)}, 800);
		return false;
	},
	function() {
		clearInterval(IntervalId);
		return false;
	});
	
	/* Add margin-right:0 fo Archive block */
	jQuery('.archive:nth-child(2n)').addClass('last');
	
	/* animate social links */
	jQuery('a.social_icons').each(function(index) {
        if (jQuery(this).hasClass('social_icons_right')) {
			jQuery(this).append('<span class="icon"></span>');
		} else {
			jQuery(this).prepend('<span class="icon"></span>');
		}
    });
	jQuery('#block_follow li a, #block_web_services li a').hover(function() {
		jQuery(this).stop().animate({'paddingLeft': '20px'}, 200);
	},
	function() {
		jQuery(this).stop().animate({'paddingLeft': '0'}, 200);
	});
	
	/* Change Font Size */
	var change_font_size = jQuery('#content .node section.content p, #comments .content p, #comments .date, #content .date_main');
	jQuery('#change_font_size .font_size_normal').click(function() {
		change_font_size.css({'fontSize': '12px'});
		return false;
	});
	jQuery('#change_font_size .font_size_down').click(function() {
		if (parseInt(jQuery('#content .node section.content p').css('fontSize')) > 10) {
			change_font_size.animate({'fontSize': '-=1'});
		}
		return false;
	});
	jQuery('#change_font_size .font_size_up').click(function() {
		if (parseInt(jQuery('#content .node section.content p').css('fontSize')) < 16) {
			change_font_size.animate({'fontSize': '+=1'});
		}
		return false;
	});
	
	/* Shortcodes */
	jQuery('blockquote').prepend('<span class="quote_start"></span>').append('<span class="quote_end"></span>');
	/* Content Toggle */
	jQuery('.toggle:not(.active) .toggle_content').hide();
	jQuery('.toggle .toggle_title').click(function() {
		jQuery(this).parent().find('.toggle_content').slideToggle();
		return false;
	});
	jQuery('.accordions').accordion({autoHeight: false, navigation: true}); // accordions
	jQuery('.slider ul').anythingSlider({buildStartStop: false, buildArrows: false, hashTags: false, autoPlay: true, delay: 10000}); // sliders
		
	/* Show Code */
	jQuery('.show_code a').click(function() {
		jQuery(this).parent().toggleClass('show_code_active').parent().find('.code').slideToggle();
		return false;
	});
	
	/* Extend jQuery, function toggleAttr, working with checked, disabled and readonly */
	jQuery.fn.extend({
		toggleAttr : function(attrib) {
			if ( this.attr(attrib) ) {
				this.removeAttr(attrib);
			} else {
				this.attr(attrib, attrib);
			}
			return this;
		}
	});
	
	/* Block LogIn,Register */
	jQuery('#block_header_login a, #block_login_register .close a').click(function() {
		jQuery('#block_login_register').slideToggle('fast');
		return false;
	});
	/* Replace checkbox with background-image */
	jQuery('input.checkbox_replace').wrap('<span class="checkbox_replace_wrap"></span>').parent().append('<a href="#" class="checkbox_replace_bg"></a>');
	jQuery('a.checkbox_replace_bg').click(function() {
		jQuery(this).toggleClass('checked').parent().find('input.checkbox_replace').toggleAttr('checked');
	});
	jQuery('input.checkbox_replace').change(function() {
		jQuery(this).toggleAttr('checked').parent().find('a.checkbox_replace_bg').toggleClass('checked');
	});
	
	/* Contact form */
	jQuery('#contact_form_submit').submit(function() {
		$this = jQuery(this);
		var name = $this.find('#edit-submitted-name').val();
		var email = $this.find('#edit-submitted-email').val();
		var phone = $this.find('#edit-submitted-phone').val();
		$this.find('.message').remove();
		if (name == "") {
			$this.prepend('<div class="message message_error">Error!<br/>Please enter your Name.</div>');
			$this.find('#edit-submitted-name').focus();
			return false;
		}
		var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
		if (email == "" || !emailReg.test(email)) {
			$this.prepend('<div class="message message_error">Error!<br/>Please enter your email.</div>');
			$this.find('#edit-submitted-email').focus();
			return false;
		}
		$this.prepend('<div class="message message_info">Loading<br/>Please wait...</div>');
		jQuery.ajax({
   			type: 'POST',
   			url: jQuery(this).attr('action'),
   			data: jQuery(this).serializeArray(),
   			success: function(msg){
				$this.find('.message').remove();
   				$this.prepend(msg);
			},
			timeout: 30000,
			error: function(jqXHR, textStatus, errorThrown){
				$this.find('.message').remove();
				$this.prepend('<div class="message message_error">Error!<br/>'+jqXHR.status+' - '+jqXHR.statusText+'</div>');
			}
		});
		return false;	
	});
});
