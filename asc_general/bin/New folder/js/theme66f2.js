(function ($) {
 "use strict";
    

/*----------------------------
    Toogle Search
------------------------------ */
    // Handle click on toggle search button
    $('.header-search').on('click', function() {
        $('.tsearch').toggleClass('open');
        return false;
    });
    
    // Handle click on search submit button
    $('#search-edu input[type=submit]').on('click', function() {
        $('.tsearch').toggleClass('open');
        return true;
    });
    
    // Clicking outside the search form closes it
    $(document).on('click', function(event) {
        var target = $(event.target);
        
        if (!target.is('.header-search') && !target.closest('.tsearch').size()) {
            $('.tsearch').removeClass('open');
        }
    });
	
	$(".search-menu i.search").on('click', function(){
        $("i.search").addClass('hidden');
        $('i.close').removeClass('hidden');
    });
	$(".search-menu i.close").on('click', function(){
        $("i.close").addClass('hidden');
        $('.search-menu i.search').removeClass('hidden');
    });	

/*----------------------------
    Owl active
------------------------------ */  
    $(".class-carousel").owlCarousel({
        autoPlay: false, 
        slideSpeed:2000,
        pagination:false,
        navigation:true,	  
        items : 3,
        navigationText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
        itemsDesktop : [1199,3],
        itemsDesktopSmall : [980,2],
        itemsTablet: [768,2],
        itemsMobile : [479,1],
    });
/*----------------------------
    Blog Carousel
------------------------------ */  
    $(".blog-carousel").owlCarousel({
        autoPlay: false, 
        slideSpeed:2000,
        pagination:false,
        navigation:true,
        navigationText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
		items : 3,
        itemsCustom : [
        [0, 1],
        [450, 1],
        [480, 1],
        [600, 1],
        [700, 2],
        [768, 2],
        [992, 3],
		[1199, 3]
      ],
    });
/*----------------------------
    Team Carousel
------------------------------ */  
    $(".team-carousel").owlCarousel({
        autoPlay: false, 
        slideSpeed:2000,
        pagination:false,
        navigation:true,	  
        items : 4,
        navigationText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
        itemsDesktop : [1199,4],
        itemsDesktopSmall : [980,3],
        itemsTablet: [768,2],
        itemsMobile : [479,1],
    }); 
/*----------------------------
    testimonial Carousel
------------------------------ */  
    $(".testimonial-carousel").owlCarousel({
        autoPlay: false, 
        slideSpeed:2000,
        pagination:false,
        navigation:true,	  
        items : 2,
        navigationText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
        itemsDesktop : [1199,2],
        itemsDesktopSmall : [980,2],
        itemsTablet: [768,1],
        itemsMobile : [479,1],
    });    
/*----------------------------
    team Column Carousel
------------------------------ */  
    $(".teachers-column-carousel").owlCarousel({
        autoPlay: false, 
        slideSpeed:2000,
        pagination:false,
        navigation:true,	  
        items : 4,
        navigationText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
        itemsDesktop : [1199,4],
        itemsDesktopSmall : [980,3],
        itemsTablet: [768,2],
        itemsMobile : [479,1],
    }); 

/*----------------------------
    Testimonial Details Carousel
------------------------------ */  
    $(".client-speech-carousel").owlCarousel({
        autoPlay: false, 
        slideSpeed:2000,
        pagination:true,
        navigation:false,	  
        items : 2,
        itemsCustom : [
        [0, 1],
        [450, 1],
        [480, 1],
        [600, 1],
        [700, 2],
        [768, 2],
        [992, 2],
		[1199, 2]
      ],
    });
    /*----------------------------
    Brand Carousel
------------------------------ */  
    $(".brand-carousel").owlCarousel({
        autoPlay: false, 
        slideSpeed:2000,
        pagination:false,
        navigation:false,   
        items : 6,
        itemsDesktop : [1199,5],
        itemsDesktopSmall : [980,5],
        itemsTablet: [768,4],
        itemsMobile : [479,2],
    });
/*--------------------------
    Countdown
---------------------------- */	
    $('[data-countdown]').each(function() {
        var $this = $(this), finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function(event) {
        $this.html(event.strftime('<div class="cdown days"><span class="counting">%-D</span>days</div><div class="cdown hours"><span class="counting">%-H</span>hrs</div><div class="cdown minutes"><span class="counting">%M</span>mins</div><div class="cdown seconds"><span class="counting">%S</span>secs</div>'));
        });
    });	
    
/*--------------------------
    ScrollUp
---------------------------- */	
	$.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    }); 	   
    
/*--------------------------
    Counter Up
---------------------------- */	
    $('.counter').counterUp({
        delay: 70,
        time: 5000
    });    
       
/*--------------------------------
	Testimonial Small Carousel
-----------------------------------*/
    $('.testimonial-small-text-slider').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        draggable: false,
        fade: true,
        asNavFor: '.slider-nav'
    });
/*------------------------------------
	Testimonial Small Carousel as Nav
--------------------------------------*/
    $('.testimonial-small-image-slider').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.testimonial-small-text-slider',
        dots: false,
        arrows: false,
        centerMode: true,
        focusOnSelect: true,
        centerPadding: '10px',
        responsive: [
            {
              breakpoint: 450,
              settings: {
                dots: false,
                slidesToShow: 3,  
                centerPadding: '0px',
                }
            },
            {
              breakpoint: 420,
              settings: {
                autoplay: true,
                dots: false,
                slidesToShow: 1,
                centerMode: false,
                }
            }
        ]
    });
    
/*--------------------------
    Mix It Up
---------------------------- */	
    $('.filter-items').mixItUp(); 
 
 $( ".header-overlay" ).siblings( ".breadcrumb-banner-area" ).addClass("transparent-breadcrumb");
/*--------------------------
    Venubox
---------------------------- */	
    $('.venobox').venobox({    
        border: '10px',          
        titleattr: 'data-title',  
        numeratio: true,           
        infinigall: true      
    });
/*--------------------------
    WOW
---------------------------- */		
new WOW().init();


/*--------------------------
	Portfolio Isotope
---------------------------- */
	$('.new').imagesLoaded( function() {
		if($.fn.isotope){
			var $portfolio = $('.new');
			$portfolio.isotope({
				itemSelector: '.grid-item',
				filter: '*',
				resizesContainer: true,
				layoutMode: 'masonry',
				transitionDuration: '0.8s'			
			});

			$('.filter-menu li').on('click', function(){
				$('.filter-menu li').removeClass('current_menu_item');
				$(this).addClass('current_menu_item');
				var selector = $(this).attr('data-filter');
				$portfolio.isotope({
					filter: selector,
				});
			});
		};
	});		


/*----------------------------
        text-animation
    ------------------------------ */  
    $('.vocation-ani-heading').textillate({
      loop: true,
      in: {
        delay: 40,
      },
      out: {
        delay: 50,
      },
    });
    
    /*----------------------------
        text-animation
    ------------------------------ */  
    $('.slider-subtitle,.copy-container .excerpt').textillate({
      loop: true,
      in: {
        delay: 50,
      },
      out: {
        delay: 70,
      },
    });





    
})(jQuery); 