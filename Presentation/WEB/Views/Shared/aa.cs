//@using Microsoft.AspNetCore.Identity;
//@using Wreade.Domain.Enums;
//@using Wreade.Persistence.Implementations.Services;
//@inject LayoutService service
//@inject UserManager<AppUser> _userManager;
//@{
//    Dictionary<string, string> settings = await service.GetSettingsAsync();
//    ICollection<Category> categories = await service.GetCategoriesAsync();
//    AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
//}
//< !DOCTYPE html >
//< html lang = "en" >
//< head style = "background-image:url(/assets/images/bggalaxy.jpg);
//     background - repeat:no - repeat;
//background - size:cover; ">
//< meta charset = "utf-8" >

//< meta http - equiv = "X-UA-Compatible" content = "IE=edge" >

//< meta name = "viewport" content = "width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" >


//< title > Wreade </ title >


//< !--Fav Icon-- >

//< link rel = "icon" href = "~/assets/images/logo/logo-no-background.png" type = "image/x-icon" >


//< !--Google Fonts-- >

//< link href = "https://fonts.googleapis.com/css2?family=Amatic+SC:wght@400;700&display=swap" rel = "stylesheet" >

//< link href = "https://fonts.googleapis.com/css2?family=Rubik:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel = "stylesheet" >


//< !--Stylesheets-- >

//< link href = "~/assets/assets/css/font-awesome-all.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/flaticon.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/owl.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/bootstrap.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/jquery.fancybox.min.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/animate.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/color.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/style.css" rel = "stylesheet" >

//< link href = "~/assets/assets/css/responsive.css" rel = "stylesheet" >


//</ head >



//< !--page wrapper-- >
//< body >


//< div class= "boxed_wrapper" >



//   < !--preloader-- >

//   < div class= "preloader" >

//       < div class= "boxes" >

//           < div class= "box" >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//           </ div >

//           < div class= "box" >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//           </ div >

//           < div class= "box" >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//           </ div >

//           < div class= "box" >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//               < div ></ div >

//           </ div >

//       </ div >

//   </ div >

//   < !--preloader end-- >

//   < !--main header-- >

//   < header class= "main-header header-style-one" style = "background-image:url(/assets/images/bggalaxy.jpg);
//     background - repeat:no - repeat;
//background - size:cover; ">
//       < div class= "header-lower" >

//           < div class= "logo-box" >

//               < figure class= "logo" >< a href = "index.html" >< img src = "~/assets/images/logo/" alt = "" ></ a ></ figure >

//           </ div >

//           < div class= "outer-box" >

//               < div class= "menu-area" >

//                   < !--Mobile Navigation Toggler-->
//						<div class= "mobile-nav-toggler" >

//                            < i class= "icon-bar" ></ i >

//                            < i class= "icon-bar" ></ i >

//                            < i class= "icon-bar" ></ i >

//                        </ div >

//                        < nav class= "main-menu navbar-expand-md navbar-light" >

//                            < div class= "collapse navbar-collapse show clearfix" id = "navbarSupportedContent" >

//                                < ul class= "navigation clearfix" >
//                                    @if(!User.Identity.IsAuthenticated)

//                                    {

//                                        < li class= "dropdown" >

//                                            < a asp - action = "register" asp - controller = "appuser" > REGISTER </ a >

//                                            < ul >


//                                                < li >< a asp - action = "login" asp - controller = "appuser" > Login </ a ></ li >


//                                            </ ul >

//                                        </ li >
//									}

//                                    else
//{

//                                        < li class= "dropdown" >
//                                            @if(user != null && user.MainImage != null)

//                                            {

//                                                < img src = "~/assets/images/@(user.MainImage)" style = "width:30px; height:30px; border-radius:15px;" />

//                                            }

//                                            else
//{

//                                                < span > @User.Identity.Name </ span >


//                                            }


//                                            < ul >


//                                                < li >< a asp - action = "user" asp - controller = "profile" asp - route - username = "@user.UserName" > My Profile </ a ></ li >
//                                                @if(User.IsInRole("Admin"))

//                                                {

//                                                    < li >< a asp - action = "index" asp - controller = "dashboard" asp - route - area = "manage" > DashBoard </ a ></ li >


//                                                }

//                                                else
//{


//                                                    < li >< a asp - action = "user" asp - controller = "profile" asp - route - username = "@user.UserName" > My Library </ a ></ li >

//                                                }

//                                                < li >< a asp - action = "user" asp - controller = "profile" asp - route - username = "@user.UserName" > Settings </ a ></ li >

//                                                < li >< a asp - action = "appuser" asp - controller = "logout" > Logout </ a ></ li >


//                                            </ ul >

//                                        </ li >
//									}


//                                    < li class= "current dropdown" >

//                                        < a href = "index.html" >< span > Category </ span ></ a >
//                                        @if(categories.Count > 0)

//                                        {

//                                            < div class= "megamenu" >

//                                                < div class= "row clearfix" >

//                                                    < ul >

//                                                        < li >

//                                                            < span >

//                                                                < h4 > Categories </ h4 >

//                                                            </ span >

//                                                        </ li >

//                                                    </ ul >

//                                                </ div >


//                                                < div class= "row clearfix" >
//                                                    @foreach(var item in categories)

//                                                    {

//                                                        < div class= "col-lg-3 column" >

//                                                            < ul >

//                                                                < li >< a href = "about-element-1.html" > @item.Name </ a ></ li >

//                                                            </ ul >

//                                                        </ div >
//													}

//                                                </ div >

//                                            </ div >

//										}

//                                    </ li >

//                                    < li class= "dropdown" >

//                                        < a href = "index.html" > Activities </ a >

//                                        < ul >
//                                            @if(User.Identity.IsAuthenticated)

//                                            {
//    @if(User.IsInRole("Author"))

//                                                {

//                                                    < li >< a asp - action = "index" asp - controller = "book" > book </ a ></ li >

//                                                }
//}

//                                            < li >< a href = "climbing.html" > Climbing </ a ></ li >

//                                            < li >< a href = "adventure.html" > Adventure </ a ></ li >

//                                            < li >< a href = "camping.html" > Camping </ a ></ li >

//                                            < li >< a href = "diving.html" > Diving </ a ></ li >

//                                            < li >< a href = "parachute.html" > Parachute </ a ></ li >

//                                            < li >< a href = "throwing.html" > Throwing </ a ></ li >

//                                        </ ul >

//                                    </ li >

//                                    < li class= "dropdown" >

//                                        < a href = "index.html" > Pages </ a >

//                                        < ul >

//                                            < li >< a href = "about.html" > About Us </ a ></ li >

//                                            < li >< a href = "team.html" > Our Team </ a ></ li >

//                                            < li >< a href = "gallery.html" > Our Gallery </ a ></ li >

//                                            < li >< a href = "error.html" > 404 </ a ></ li >

//                                        </ ul >

//                                    </ li >

//                                    < li class= "dropdown" >

//                                        < a href = "index.html" > Elements </ a >

//                                        < div class= "megamenu" >

//                                            < div class= "row clearfix" >

//                                                < div class= "col-lg-4 column" >

//                                                    < ul >

//                                                        < li >< h4 > Elements 1 </ h4 ></ li >

//                                                        < li >< a href = "about-element-1.html" > About Block 01</a></li>
//														<li><a href="about-element-2.html">About Block 02</a></li>
//														<li><a href="about-element-3.html">About Block 03</a></li>
//														<li><a href="activities-element-1.html">Activities Block 01</a></li>
//														<li><a href="activities-element-2.html">Activities Block 02</a></li>
//														<li><a href="news-element-1.html">News Block 01</a></li>
//														<li><a href="news-element-2.html">News Block 02</a></li>
//														<li><a href="team-element-1.html">Team Block 01</a></li>
//													</ul>
//												</div>
//												<div class= "col-lg-6 column" >

//                                                    < ul >

//                                                        < li >< h4 > Elements 2 </ h4 ></ li >

//                                                        < li >< a href = "team-element-2.html" > Team Block 02</a></li>
//														<li><a href="feature-element-1.html">Feature Block 01</a></li>
//														<li><a href="feature-element-2.html">Feature Block 02</a></li>
//														<li><a href="video-element.html">Video Block</a></li>
//														<li><a href="gallery-element.html">Gallery Block</a></li>
//														<li><a href="testimonial-element.html">Testimonial Block</a></li>
//														<li><a href="cta-element.html">Cta Block</a></li>
//														<li><a href="clients-element.html">Clients Block</a></li>
//													</ul>
//												</div>
//											</div>
//										</div>
//									</li>
//									@if (User.Identity.IsAuthenticated)
//									{
//    @if(User.IsInRole("Author"))

//                                        {

//                                            < li class= "dropdown" >

//                                                < span > Write </ span >

//                                                < ul >

//                                                    < li >< a asp - action = "create" asp - controller = "book" asp - route - id = "@user.Id" > Create A New Story</a></li>
//													@if (user.Books is not null)
//													{

//                                                        < li >< a asp - action = "index" asp - controller = "book" > My stories </ a ></ li >


//                                                    }

//                                                </ ul >

//                                            </ li >
//										}
//									}

//                                    else
//{

//                                        < li class= "current" >< a href = "contact.html" > Contact </ a ></ li >

//									}


//                                </ ul >

//                            </ div >


//                        </ nav >

//                    </ div >

//                    < div class= "menu-right-content" >


//                        < div class= "search-box-outer" >

//                            < div class= "dropdown" >

//                                < button class= "search-box-btn" type = "button" id = "dropdownMenu3" data - toggle = "dropdown" aria - haspopup = "true" aria - expanded = "false" >< i class= "flaticon-magnifying-glass" ></ i ></ button >

//                                < div class= "dropdown-menu search-panel" aria - labelledby = "dropdownMenu3" >

//                                    < div class= "form-container" >

//                                        < form method = "post" action = "blog.html" >

//                                            < div class= "form-group" >

//                                                < input type = "search" name = "search-field" value = "" placeholder = "Search...." required = "" >

//                                                < button type = "submit" class= "search-btn" >< span class= "fas fa-search" ></ span ></ button >

//                                            </ div >

//                                        </ form >

//                                    </ div >

//                                </ div >

//                            </ div >

//                        </ div >


//                    </ div >

//                </ div >

//            </ div >


//            < !--sticky Header-- >

//            < div class= "sticky-header" >

//                < div class= "outer-box" >

//                    < div class= "menu-area" >

//                        < nav class= "main-menu clearfix" >

//                            < !--Keep This Empty / Menu will come through Javascript-->
//						</nav>
//					</div>
//					<div class= "menu-right-content" >


//                        < div class= "search-box-outer" >

//                            < div class= "dropdown" >

//                                < button class= "search-box-btn" type = "button" id = "dropdownMenu4" data - toggle = "dropdown" aria - haspopup = "true" aria - expanded = "false" >< i class= "flaticon-magnifying-glass" ></ i ></ button >

//                                < div class= "dropdown-menu search-panel" aria - labelledby = "dropdownMenu4" >

//                                    < div class= "form-container" >

//                                        < form method = "post" action = "blog.html" >

//                                            < div class= "form-group" >

//                                                < input type = "search" name = "search-field" value = "" placeholder = "Search...." required = "" >

//                                                < button type = "submit" class= "search-btn" >< span class= "fas fa-search" ></ span ></ button >

//                                            </ div >

//                                        </ form >

//                                    </ div >

//                                </ div >

//                            </ div >

//                        </ div >

//                        < div class= "cart-box" >< a href = "index.html" >< i class= "flaticon-shopping-cart" ></ i ></ a ></ div >

//                    </ div >

//                </ div >

//            </ div >

//        </ header >

//        < !--main - header end-- >

//        < !--Mobile Menu-- >

//        < div class= "mobile-menu" style = "background-image:url(/assets/images/bggalaxy.jpg);
//     background - repeat:no - repeat;
//background - size:cover; ">
//       < div class= "menu-backdrop" ></ div >

//       < div class= "close-btn" >< i class= "fas fa-times" ></ i ></ div >


//       < nav class= "menu-box" >

//           < div class= "nav-logo" >< a href = "index.html" >< img src = "assets/images/logo-2.png" alt = "" title = "" ></ a ></ div >

//           < div class= "menu-outer" >< !--Here Menu Will Come Automatically Via Javascript / Same Menu as in Header--></div>
//				<div class= "contact-info" >

//                    < h4 > Contact Info </ h4 >

//                    < ul >

//                        < li > Chicago 12, Melborne City, USA</li>
//						<li><a href="tel:+8801682648101">+88 01682648101</a></li>
//						<li><a href="mailto:info@example.com">info@example.com</a></li>
//					</ul>
//				</div>
//				<div class= "social-links" >

//                    < ul class= "clearfix" >

//                        < li >< a href = "index.html" >< span class= "fab fa-twitter" ></ span ></ a ></ li >

//                        < li >< a href = "index.html" >< span class= "fab fa-facebook-square" ></ span ></ a ></ li >

//                        < li >< a href = "index.html" >< span class= "fab fa-pinterest-p" ></ span ></ a ></ li >

//                        < li >< a href = "index.html" >< span class= "fab fa-instagram" ></ span ></ a ></ li >

//                        < li >< a href = "index.html" >< span class= "fab fa-youtube" ></ span ></ a ></ li >

//                    </ ul >

//                </ div >

//            </ nav >

//        </ div >


//        < !--End Mobile Menu -->
//		<section class= "banner-section centred" style = "background-image:url(/assets/images/bggalaxy.jpg);
//     background - repeat:no - repeat;
//background - size:cover; ">
//       < div class= "pattern-layer" style = "background-image: url(assets/images/shape/shape-1.png);" ></ div >



//   </ section >
//   @RenderBody()
//   < !--main - footer-- >

//   < footer class= "main-footer" style = "background-image:url(/assets/images/bg2.jpg);
//     background - repeat:no - repeat;
//background - size:cover; ">
//       < div class= "pattern-layer" style = "background-image: url(assets/images/shape/shape-13.png);" ></ div >

//       < div class= "auto-container" >

//           < div class= "footer-top" >

//               < div class= "top-inner" >

//                   < div class= "text" >

//                       < h5 > Send Email </ h5 >

//                       < h3 >< a href = "mailto:needhelp@company.com" > needhelp@company.com </ a ></ h3 >

//                   </ div >

//                   < figure class= "footer-logo" >< a href = "index.html" >< img src = "~/assets/assets/images/footer-logo.png" alt = "" ></ a ></ figure >

//                   < div class= "text" >

//                       < h5 > Call Anytime </ h5 >

//                       < h3 >< a href = "tel:12463330079" > +1 - (246) 333 - 0079 </ a ></ h3 >

//                   </ div >

//               </ div >

//           </ div >

//           < div class= "widget-section" >

//               < div class= "row clearfix" >

//                   < div class= "col-lg-2 col-md-6 col-sm-12 footer-column" >

//                       < div class= "footer-widget links-widget" >

//                           < div class= "widget-title" >

//                               < h4 > Explore </ h4 >

//                           </ div >

//                           < div class= "widget-content" >

//                               < ul class= "links-list clearfix" >

//                                   < li >< a href = "index.html" > Meet Our Team</a></li>
//										<li><a href = "index.html" > What We Do</a></li>
//										<li><a href = "index.html" > Latest News</a></li>
//										<li><a href = "index.html" > Contact </ a ></ li >

//                               </ ul >

//                           </ div >

//                       </ div >

//                   </ div >

//                   < div class= "col-lg-3 col-md-6 col-sm-12 footer-column" >

//                       < div class= "footer-widget links-widget ml-70" >

//                           < div class= "widget-title" >

//                               < h4 > Activities </ h4 >

//                           </ div >

//                           < div class= "widget-content" >

//                               < ul class= "links-list clearfix" >

//                                   < li >< a href = "index.html" > Tree Climbing </ a ></ li >

//                                   < li >< a href = "index.html" > Cross the River</a></li>
//										<li><a href = "index.html" > Mountain Boarding</a></li>
//										<li><a href = "index.html" > Parachute </ a ></ li >

//                               </ ul >

//                           </ div >

//                       </ div >

//                   </ div >

//                   < div class= "col-lg-3 col-md-6 col-sm-12 footer-column" >

//                       < div class= "footer-widget contact-widget" >

//                           < div class= "widget-title" >

//                               < h4 > Contact </ h4 >

//                           </ div >

//                           < div class= "widget-content" >

//                               < p > 60 road, broklyn golden street new york.USA</p>

//                               < ul class= "social-links clearfix" >

//                                   < li >< a href = "index.html" >< i class= "fab fa-twitter" ></ i ></ a ></ li >

//                                   < li >< a href = "index.html" >< i class= "fab fa-facebook-f" ></ i ></ a ></ li >

//                                   < li >< a href = "index.html" >< i class= "fab fa-pinterest-p" ></ i ></ a ></ li >

//                                   < li >< a href = "index.html" >< i class= "fab fa-instagram" ></ i ></ a ></ li >

//                               </ ul >

//                           </ div >

//                       </ div >

//                   </ div >

//                   < div class= "col-lg-4 col-md-6 col-sm-12 footer-column" >

//                       < div class= "footer-widget newsletter-widget" >

//                           < div class= "widget-title" >

//                               < h4 > Newsletter </ h4 >

//                           </ div >

//                           < div class= "widget-content" >

//                               < p > Subsrcibe for our upcoming latest articles and news resources </ p >

//                               < form action = "contact.html" method = "post" class= "newsletter-form" >

//                                   < div class= "form-group" >

//                                       < input type = "email" name = "email" placeholder = "Email address" required >

//                                       < button type = "submit" >< i class= "fas fa-paper-plane" ></ i ></ button >

//                                   </ div >

//                               </ form >

//                           </ div >

//                       </ div >

//                   </ div >

//               </ div >

//           </ div >

//       </ div >

//       < div class= "footer-bottom centred" >

//           < div class= "auto-container" >

//               < div class= "copyright" >

//                   < p > &copy; Copyright 2022 by <a href="index.html">company.com</a></p>
//					</div>
//				</div>
//			</div>
//		</footer>
//		<!-- main-footer end -->
//		<!-- scroll to top -->
//		<button class= "scroll-top scroll-to-target" data - target = "html" >

//            < i class= "fal fa-long-arrow-up" ></ i >

//        </ button >

//    </ div >



//    < !--jequery plugins-- >

//    < script src = "~/assets/assets/js/jquery.js" ></ script >

//    < script src = "~/assets/assets/js/popper.min.js" ></ script >

//    < script src = "~/assets/assets/js/bootstrap.min.js" ></ script >

//    < script src = "~/assets/assets/js/owl.js" ></ script >

//    < script src = "~/assets/assets/js/wow.js" ></ script >

//    < script src = "~/assets/assets/js/validation.js" ></ script >

//    < script src = "~/assets/assets/js/jquery.fancybox.js" ></ script >

//    < script src = "~/assets/assets/js/appear.js" ></ script >

//    < script src = "~/assets/assets/js/scrollbar.js" ></ script >

//    < script src = "~/assets/assets/js/parallax-scroll.js" ></ script >

//    < script src = "~/assets/assets/js/isotope.js" ></ script >


//    < !--main - js-- >

//    < script src = "~/assets/assets/js/script.js" ></ script >

//</ body >

//< !--End of.page_wrapper-- >
//</ html >
