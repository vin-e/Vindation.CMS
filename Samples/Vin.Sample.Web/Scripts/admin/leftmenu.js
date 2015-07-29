$(window).load(function () {
 	$('.sidenav.left ul > li').click(function (ev) {
 	    $(this).find('>ul').fadeToggle();
 	    ev.stopPropagation();
 	});
 	$('.sidenav.left ul > li a.back').click(function (ev) {
 	    $(this).parent().parent().fadeOut();
 	    ev.stopPropagation();
 	});
});
$(window).on("load resize", function () {
  if ($(window).width() <= 768) {
      $(document).on("swiperight", function () {
          $(".sidenav.left").addClass('sidenavhover');
          $(".overlay").fadeIn();
          $(".sidenav.left li.user").addClass('usershow');
      });
      $(document).on("swipeleft", function () {
          $(".sidenav.left").removeClass('sidenavhover');
          $(".sidenav.left ul > li ul").hide();
          $(".overlay").fadeOut();
          $(".sidenav.left li.user").removeClass('usershow');
      });
      $(".overlay").click(function () {
          $(".sidenav.left").removeClass('sidenavhover');
          $(".sidenav.left ul > li ul").hide();
          $(".overlay").fadeOut();
          $(".sidenav.left li.user").removeClass('usershow');
      });
  }
  if ($(window).width() >= 769) {
      $(".sidenav.left").mouseover(function () {
          $(".sidenav.left").addClass('sidenavhover');
          $(".sidenav.left li.user").addClass('usershow');
          $(".overlay").fadeIn();
      });
      $(".overlay").click(function () {
          $(".sidenav.left").removeClass('sidenavhover');
          $(".sidenav.left ul > li ul").slideUp(100);
          $(".sidenav.left li.user").removeClass('usershow');
          $(".overlay").fadeOut();
      });
  }  if ($(window).height() < 458) {
      $(".sidenav.left ul > li.logout").css('position', 'relative');
      $(".sidenav.left ul > li.logout").css('border-top', '0');
  }
  if ($(window).height() > 458) {
      $(".sidenav.left ul > li.logout").css('position', 'absolute');
      $(".sidenav.left ul > li.logout").css('border-top', '1px solid #eeeeee');
  }
  if ($(window).height() < 588) {
      $(".sidenav.left.sidenavhover ul > li.logout").css('position', 'relative');
      $(".sidenav.left.sidenavhover ul > li.logout").css('border-top', '0');
  }
  if ($(window).height() > 588) {
      $(".sidenav.left.sidenavhover ul > li.logout").css('position', 'absolute');
      $(".sidenav.left.sidenavhover ul > li.logout").css('border-top', '1px solid #eeeeee');
  }
});