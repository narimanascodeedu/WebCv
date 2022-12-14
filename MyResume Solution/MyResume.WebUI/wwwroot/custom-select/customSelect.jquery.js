(function( $ ){
	$.fn.customSelect = function(options){
		if(typeof options.identifier === "undefined" || options.identifier == ""){
			options.identifier = Math.floor((Math.random() * 8645));
		}
	
		$(options.selector).after(
			"<div id='jqcs_s_"+options.identifier+"' class='jqcs_select "+options.cssClass+"'>"+
				"<div class='jqcs_value'><p class='jqcs_placeholder'>"+options.placeholder+"</p></div>"+
				"<div class='jqcs_arrow'></div>"+
			"</div>"+
			"<div id='jqcs_o_" + options.identifier+"' class='jqcs_options'></div>"
		);
		
		$('.jqcs_select' + ' .jqcs_arrow').width($('.jqcs_select').height());
		
		
		for(var i = 0; i < options.options.length; i++){
			var currenthtml = $('.jqcs_options').html();
			var template = options.template;
			
			for(var j = 0; j < options.options[i].length; j++){
				var regex = new RegExp("\\$"+j, "g");
				template = template.replace(regex, options.options[i][j]);
			}
										
			$('.jqcs_options').html(currenthtml + template);
		}
		
		$('.jqcs_select').click(function (e) {
			console.log(e.currentTarget);
			e.stopPropagation();
			if (e.currentTarget == "block") {
				e.currentTarget.siblings('.jqcs_options').slideUp();
				//$('.jqcs_options').slideUp();
				$($('.jqcs_select' +' .jqcs_arrow')[0]).removeClass('rotated');
			} else {
				//e.currentTarget.siblings('.jqcs_options').slideDown();
				$('.jqcs_options').slideDown();
				$($('.jqcs_select' +' .jqcs_arrow')[0]).addClass('rotated');
			}
		});
		
		$('.jqcs_option').click(function(e){
			$("[name^='select']")[0].value = $(this).data('select-value');
			$($('.jqcs_select' +' .jqcs_value')[0]).html(this.outerHTML);
		});
		
		$(window).click(function(e){
			$('.jqcs_options').slideUp();
			$($('.jqcs_select' +' .jqcs_arrow')[0]).removeClass('rotated');
		});
	}
})( jQuery );