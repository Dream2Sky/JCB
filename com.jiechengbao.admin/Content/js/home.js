 
        $(document).ready(function(){
          $(".List").click(function(){
            var p = $(this).children("span").attr("class");
            if(p.indexOf("rotate")>0){
              $(this).next().toggle();
              $(".arrow").removeClass("rotate");
            }else{
              $(".drop").slideUp();
              $(this).next().toggle();
              $(".arrow").removeClass("rotate");
              $(this).children("span").addClass("rotate");  
            }
           });
        });
    