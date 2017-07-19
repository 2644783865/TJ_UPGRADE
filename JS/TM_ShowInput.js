//表格中的某一文本框由于太窄，输入不方便，通过弹出一个Div来进行修改录入
function ShowInput(obj)
{
   var oDiv=document.createElement("div");
   oDiv.style.border="#B9D3EE 3px solid";
   oDiv.style.width="320px";
   oDiv.style.height="80px";
   oDiv.style.backgroundColor="#E1E1E1";
   oDiv.style.position="absolute";
   oDiv.style.padding="2px";
   
   //原来Obj的背景颜色
   var oldbackground=obj.style.backgroundColor;
   obj.style.backgroundColor="#55DF55";
   
   obj.parentNode.appendChild(oDiv);
   
   var oInput=document.createElement("textarea");
   oInput.style.width="300px";
   oInput.style.height="72px";
   oDiv.appendChild(oInput);
   oInput.value=obj.value;
   oInput.select();
   
   try
   {
      if(typeof(eval(grControlFocus))=="function")
      {
         oInput.onkeydown=function()
         {
            var key=window.event.keyCode;
            if(key==37||key==38||key==39||key==40)
            {
               obj.focus();
            }
         }
      }

   }
   catch(e)
   { 
      ;
   }
   
   oInput.onblur = function()
   {
      obj.style.backgroundColor=oldbackground;
      obj.value=oInput.value;
      obj.parentNode.removeChild(oDiv);
   }
   
}

//<textarea id="TextArea1" cols="20" rows="2"></textarea>