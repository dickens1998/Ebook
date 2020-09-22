autoDiv();
IEaddFixClass();

// 下拉列表
$(".dropdown_group").click(function(event) {
	event.stopPropagation();
	$(this).find(".dropdown_select").toggle();
	$(this).parent().siblings().find(".dropdown_select").hide();
});
$(document).click(function(event) {
	var eo = $(event.target);
	if ($(".dropdown_group").is(":visible") && eo.attr("class") != "dropdown_select" && !eo.parent(".dropdown_select").length) {
		$('.dropdown_select').hide();
		// console.log(!eo.parent(".dropdown_select").length);
	}
});
// 下拉列表--赋值给文本框
$(".dropdown_select li a").click(function() {
	var value = $(this).text();
	$(this).parents(".dropdown_select").siblings(".dropdown_text").text(value);
	$(".select_value").val(value)
});


// $(".mod_appr_table tr:even").css({
// 	"background-color": '#f1f1f1'
// });

// tab切换
$('.hrc_tab_item').on('click',function(e){
	e.stopPropagation();
	$(this).addClass('active').siblings().removeClass('active');
})


//全选
function selectAllTr(){
	var $toggle = $('.check_all'),
		$checkbox = $('table').find('.mod_check[type="checkbox"]').not('.check_all');
		
	//全选和反选
	$toggle.on('click', function() {
		var $this = $(this);
			$td = $this.parents('table').find('.mod_check[type="checkbox"]').not('.check_all');
		if($this.is(":checked")){
	       $td.prop("checked", true); 
        } else {// 取消全选  
           $td.prop("checked", false); 
        }  
	})
	
	//取消全选
	$checkbox.on('click', function() {
		var $this = $(this);
			$all = $this.parents('table').find('.check_all');
			
		if($this.is(":checked")){
			
        } else { 
           $all.prop("checked", false); 
        }  
	})
}

$('.triggle-success').on('click', function(){
	$('.pop_cover').show();
	$('.success-status').show();
	setTimeout(closeStatusWrap, 1000);
});

$('.triggle-loading').on('click', function(){
	$('.pop_cover').show();
	$('.loading-status').show();
});

$('.triggle-error').on('click', function(){
	$('.pop_cover').show();
	$('.error-status').show();
	// setTimeout(closeStatusWrap, 2000);
});
$('.triggle-error-detail').on('click', function(){
	$('.pop_cover').show();
	$('.error-status-detail').show();
	// setTimeout(closeStatusWrap, 2000);
});

$('.triggle-warn').on('click', function(){
	$('.pop_cover').show();
	$('.warn-status').show();
	// setTimeout(closeStatusWrap, 2000);
});
$('.triggle-warn-detail').on('click', function(){
	$('.pop_cover').show();
	$('.warn-status-detail').show();
	// setTimeout(closeStatusWrap, 2000);
});
function closeStatusWrap(){
	$('.pop_cover').hide();
	$('.hrc-status-wrap').hide();
}
$('.triggle-pop').on('click', function(){
	$('.pop_cover').show();
	$('.pop_wrap').show();
})

// 弹窗操作
$(".pop_head .pop_close").click(function() {
	$(this).parents(".pop_wrap").hide(300);
	$('.pop_cover').hide();
});
//提示操作
$('.hrc-status-wrap .icon_close').on('click', function(){
	$(this).parents(".hrc-status-wrap").hide(300);
	$('.pop_cover').hide();
})

// 折叠表单标题
$('.hrc-collapse-title').on('click', function(){
	$(this).find('.hrc-glyphicon').toggleClass('hrc-tree-collapse');
})

/* 绩效考核、综合评估部分折叠与收起 */
$('.collapsable-title').on('click', function(){
	$this = $(this);
	$parent = $(this).parent('.collapsable-area');
	$this.toggleClass('collapsable');
	$parent.find('.collapsable-cont').fadeToggle();
});


// $('.tips-wrap .hrc-a-btn').hover(function(e){
// 	var $this = $(e.target);
// 	var txt = $this.attr('title');
// 	// var x = e.originalEvent.x || e.originalEvent.layerX || 0; 
// 	// var y = e.originalEvent.y || e.originalEvent.layerY || 0; 
// 	var x = $this.offset().left;
// 	var y = $this.offset().top + 15;
// 	// console.log(txt)
// 	$('.tooltips').html(txt).show().css({
// 		top: y + 20 + 'px', 
// 		left: x + 'px'
// 	});
// }, function(){
// 	$('.tooltips').hide();
// });


// $('.tips-wrap .hrc-a-btn, .hrc-btn-group .hrc-a-btn').hover(function(e){
// 	var $this = $(e.target);
// 	console.log($this);
// 	var $parent = $this.parent('.tips-wrap');
// 	var txt = $this.prop('title');
// 	$parent.find('.tooltip').addClass('active').html(txt);
// }, function(e){
// 	var $this = $(e.target);
// 	var $parent = $this.parent('.tips-wrap');
// 	$parent.find('.tooltip').removeClass('active')
// })

//tree与右侧页面等高
function setProgressConfigTreeHeight(){
	var $cont = $('.progress_config .appr_content');
	var _height = $cont.height();
	// var _height = $cont.height() + $cont.css('paddingTop') + $cont.css('paddingBottom');
	$('.progress_config .progress_config_tree').height(_height);
	$('.progress_config .config_wrap').height(_height);
}
setProgressConfigTreeHeight();

// 窗口自适应方法
function autoDiv() {
	$(window).height() > $(".pop_wrap").height() ? $(".pop_wrap").css({
		"margin-top": -$(".pop_wrap").height()/2,
		"margin-left": -$(".pop_wrap").width()/2,
		"position": "fixed"
	}) : $(".pop_wrap").css({
		"margin-top": -$(window).height()/4,
		"margin-left": -$(".pop_wrap").width()/2,
		"position": "absolute"
	});
	
	$(".appr_sidebar").css({
		"height": $(".appr_bd_wrap").height()
	});
	$('.appraisal').css({
		'height': $('.appr_bd_wrap').height()
	});
}

//全屏
$('.window_control button').on('click', function(e){
	var $this = $(this);
	console.log($this);
	if(!$this.hasClass('disabled')){
		$this.toggleClass('disabled').siblings().toggleClass('disabled');
		$('.progress_config_tree').toggleClass('open');
	}
	
})

//IE8自适应
function IEaddFixClass(){
	if($('.lt9').length){
		var width = $(window).width();
		var $html = $('html');
		if (width > 1620){
			$html.removeClass().addClass('lt9 large');
		} else if(width < 1620 && width > 1420) {
			$html.removeClass().addClass('lt9 middle');
		} else if(width < 1420 && width > 1220) {
			$html.removeClass().addClass('lt9 small');
		} else if(width < 1220 && width > 990) {
			$html.removeClass().addClass('lt9 x-small');
		}
	}
}

$(window).resize(function() {
	autoDiv();
	IEaddFixClass();
	setProgressConfigTreeHeight();
})