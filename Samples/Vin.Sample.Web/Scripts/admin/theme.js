$(window).load(function () {
    $('.search').bind('blur', function () {
        $(".sidenav ul > li.user .search-block span").toggleClass('focus');
    });
    $('.search').bind('focus', function () {
        $(".sidenav ul > li.user .search-block span").toggleClass('focus');
    });
 	$('.header .headerclose').click(function (ev) {
 	    $(this).parent().parent().fadeOut();
 	    ev.stopPropagation();
 	});
	var headerrefresh='<div class="widgetrefresh"><span><i class="fa fa-spinner fa-spin fa-2x"></i></span></div>'
    $(".header .headerrefresh").click(function(){
        $(this).parent().after(headerrefresh);
        $(".widgetrefresh").fadeOut(2000);
    });
   $(".header .headershrink").click(function(){
	   $(this).parent().parent().toggleClass('shrinkwell');
	   $(this).find('> i').toggleClass('fa-chevron-down');
	   $(this).find('> i').toggleClass('fa-chevron-up');
	});
 	$('img').on('dragstart', function (event) {
 	    event.preventDefault();
 	});
});
$(window).on("load resize", function () {
  if ($(window).width() <= 768) {
      $(".controlshint").fadeIn(2000).fadeOut(2000);
      $(".controlshint").click(function () {
          $(this).remove();
      });
  }
});