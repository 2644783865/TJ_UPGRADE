function drag(elementToDrag,event){
   var startX=event.clientX,startY=event.clientY;//鼠标坐标
   var origX=elementToDrag.offsetLeft,origY=elementToDrag.offsetTop;//相对位置
   var deltaX=startX-origX,deltaY=startY-origY;//
   if(document.addEventListener){
     document.addEventListener("mousemove",moveHandler,true);
     document.addEventListener("mouseup",upHandler,true);

   }else if(document.attachEvent){
     elementToDrag.setCapture();
     elementToDrag.attachEvent("onmousemove",moveHandler);
     elementToDrag.attachEvent("onmouseup",upHandler);
     elementToDrag.attachEvent("onlosecaptrue",upHandler);
   }else{
     var oldmovehandler=elementToDrag.onmousemove;
     var olduphandler=elementToDrag.onmouseup;
     document.onmousemove=moveHandler;
     document.onmouseup=upHandler;
   }
   if(event.stopPropagation) event.stopPropagation;
   else event.cancekBubble=true;

   if(event.preventDefault) event.preventDefault();
   else event.returnValue=false;

   function moveHandler(e){
     if(!e) w=window.event;
    
     elementToDrag.style.left=(e.clientX-deltaX)+"px";
     elementToDrag.style.top=(e.clientY-deltaY)+"px";
     if(e.stopPropagation) e.stopPropagation();
     else e.cancelBubble=true;
   }

   function upHandler(e){
     if(!e) e=window.event;

     if(document.removeEventListener){//DOM Event model
    document.removeEventListener("mouseup",upHandler,true);
    document.removeEventListener("mousemove",moveHandler,true);
     }else if(document.detachEvent){//IE 5+ Event model
    elementToDrag.detachEvent("onlosecaptrue",upHandler);
    elementToDrag.detachEvent("onmouseup",upHandler);
    elementToDrag.detachEvent("onmousemove",moveHandler);   
    elementToDrag.releaseCapture();
     }else{//IE 4 Event Model
    document.onmouseup=olduphandler;
    document.onmousemove=oldmovehandler;
     }

     if(e.stopPropagation) e.stopPropagation();
     else e.cancelBubble=true;
   }
}


var popuped = false;
var adPopup
var popWin = function(src) { 
var div=document.createElement("div");  
div.style.width="820px";
div.style.height="450px";
//div.style.filter="Alpha(Opacity=5)";
//div.style.opacity=0.01; 
div.style.backgroundColor="#969696";
div.style.zIndex=1000;
div.style.position="absolute";
div.style.top="100px";
div.style.left="300px";
//div.style.overflow="auto";
div.style.cursor="move";
div.style.margin="auto auto";
div.style.paddingTop="25px";
div.style.textAlign="center";
div.innerHTML='<iframe frameborder=0 width=99% height=99%  src="'+src+'"></iframe>';
document.body.appendChild(div);

var a=document.createElement("a");
a.href="#";
a.innerHTML="关闭";
a.style.position="absolute";
a.style.left="760px";
a.style.fontSize="12px";
a.style.top="5px";
a.onclick=function(){
   var iframe=div.getElementsByTagName("iframe")[0];
   if(iframe){
    div.removeChild(iframe);
    iframe=null;
   }
   div.parentNode.removeChild(div);
   div=null;
   return false;
}
div.appendChild(a);
return div;
}
function windowopen(src)
{
    var div = popWin(src);
    if(document.all)
    {    
       div.onmousedown=function()
       {
        drag(this,event);
       }
    }
    else
    {
       div.addEventListener("mousedown",function(e)
       {
        drag(this,e);
       },true);
    }
}