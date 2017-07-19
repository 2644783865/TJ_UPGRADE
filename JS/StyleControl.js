/*参数说明：	
arrField—页面上控件的名称，定义成数组的形式 例如arrField[0]="txtName";
arrRemind—要提示的信息  例如 arrRemind[0]="请选择人员！";
*/
var hintColor="yellow"; //警告色
function CheckInput(arrField,arrRemind){
	len1=arrField.length; 
	len2=arrRemind.length; 
	

	if(len1!=len2) {
		alert("参数错误！");
		return false;
	}
	//输入非空验证
	for (k=0;k<len1;k++){	
		_tmpObj=document.all(arrField[k]);
		_tmpObj1=document.all(arrField[k],0);
		//_tmpObj2=document.forms[0].item(arrField[k]);

		switch(_tmpObj.tagName){  
		case "INPUT":
		if(_tmpObj1.type=="text" || _tmpObj1.type=="file" ||  _tmpObj1.type=="password")
		{   
			if(_tmpObj.value.trim()=="")
			{//alert("here kao");
				_tmpObj.style.background=hintColor;
				alert(arrRemind[k]);
				return false;
			}
			
		}
    
		if(_tmpObj1.type=="checkbox" || _tmpObj1.type=="radio"){        
			_tmpObj2=document.all(arrField[k],1);         
         		if (_tmpObj2==null) _tmpObj1.checked=true;
			else {
          //判断是否已选择
          		ischeck=0;
				for (x=0;x<_tmpObj.length;x++){			
					if(_tmpObj[x].checked){
						ischeck=1;
						break;				
					}
          		} //end for          
				if (ischeck==0){			
                		for (x=0;x<_tmpObj.length;x++) 
                            _tmpObj.style.background=hintColor;                	
                			alert(arrRemind[k]);
						return false;                	
          		} //end if           
			} //end else                 
		} //end if 
          break;           
		case "SELECT":

       		if(_tmpObj.options[_tmpObj.selectedIndex].value=="" ||
       		_tmpObj.options[_tmpObj.selectedIndex].value.trim()=="-请选择-"){
				_tmpObj.style.background=hintColor;
				alert(arrRemind[k]);return false;
			} 
           break;
           
		case "TEXTAREA":		
          if(_tmpObj.value.trim()==""){
           _tmpObj.style.background=hintColor;
           alert(arrRemind[k]);return false;}
           else
           {
               
           }
           break;           
		defalut:
            break;
		}  //end switch
	}      //end for
    
    //输入格式验证
	return ValidateForm();
}

//删除字符串前后空格
String.prototype.trim=function(){
　　 return this.replace(/(^\s*)|(\s*$)/g, "");
　　}



// JScript File
var spanText;
function ValidateForm()
{
    var i;
    var valStr="";
    //Run loop of all form controls
    for(i=0;i<=document.forms[0].length-1;i++)
    {
        tmpObj=document.forms[0].item(i);
        //Check mandetory controls
        if((tmpObj.valRule!="") && (typeof(tmpObj.valRule)!="undefined"))
        {
            //Check type of controls
            if(tmpObj.type=="text")
            {
                //Validate empty text controls
                if(tmpObj.value!="")
                {
                    //Check Validation rules
                    if(tmpObj.valRule=="IsValidEmail")//Email
                    {
                        if(IsValid(tmpObj.valRule,tmpObj.value,tmpObj.inputName)==false)
                        {
                            //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                            tmpObj.style.background=hintColor;
                            alert(spanText);return false;
                        }
                    }
                    else if(tmpObj.valRule=="IsValidURL")//网址
                    {
                        if(IsValid(tmpObj.valRule,tmpObj.value,tmpObj.inputName)==false)
                        {
                            //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                            tmpObj.style.background=hintColor;
                            alert(spanText);return false;
                        }
                    }
                    else if(tmpObj.valRule=="IsValidDate")//日期
                    {
                        if(IsValid(tmpObj.valRule,tmpObj.value,tmpObj.inputName)==false)
                        {
                            //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                            tmpObj.style.background=hintColor;
                            alert(spanText);return false;
                        }
                        else if((tmpObj.minDateVal!="") || (tmpObj.maxDateVal!=""))
                        {
                            if(Date.parse((tmpObj.value))<Date.parse((tmpObj.minDateVal)))
                            {
                                //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                                tmpObj.style.background=hintColor;
                                alert(spanText);return false;
                            }
                            else if(Date.parse((tmpObj.value))>Date.parse((tmpObj.maxDateVal)))
                            {
                                //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                                tmpObj.style.background=hintColor;
                                alert(spanText);return false;
                            }
                        }
                    }
                    else if(typeof(tmpObj.valRule)!="undefined")//数字
                    {
                        //if(IsPositiveInteger(tmpObj.value)==true)
                        if(IsValid(tmpObj.valRule,tmpObj.value,tmpObj.inputName)==false)
                        {
                            //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                            tmpObj.style.background=hintColor;
                            alert(spanText);return false;

                        }
                        else if((tmpObj.minNumVal!="") || (tmpObj.maxNumVal!=""))
                        {
                            if(parseInt(tmpObj.value)<parseInt(tmpObj.minNumVal))
                            {
                                //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                                tmpObj.style.background=hintColor;
                                alert(spanText);return false;
                            }
                            else if(parseInt(tmpObj.value)>parseInt(tmpObj.maxNumVal))
                            {
                                //valStr = ValidationMessage(valStr,tmpObj.cntTitle);
                                tmpObj.style.background=hintColor;
                                alert(spanText);return false;
                            }
                        }
                    }
                    
                }
            }
        }
    }
    return true;
}
 
 //Apply Rule and Validate
 //valRule-输入值类型
 //valCnt-输入值
 //inputName-输入值名称
 function IsValid(valRule,valCnt,inputName)
 {
    var nPattern;
    var matchVal;
    spanText = inputName ;
    switch(valRule)
    {
    case "IsPositiveInteger":
        nPattern = /^(0|[1-9][0-9]*)$/;
        spanText += " 需输入非负整数";  
        //"Only positive integer value allowed";
        break;
    case "IsPositiveDecimal":
        nPattern = /^([-+]?[0-9]*\.?[0-9]+)$/;
        spanText += " 需输入十进制数字（非0整数首位可以是0）";//"Only positive decimal value allowed";
        break;
    case "IsPositiveNDecimal":
        nPattern = /(^(0|[1-9][0-9]*)$)|((^(0?|[1-9][0-9]*)\.(0*[1-9][0-9]*)$)|(^[1-9]+[0-9]*\.0+$)|(^0\.0+$))/;
        spanText += " 需输入不带符号的非负数";// "Only positive and decimal value allowed";
        break;
    case "IsSignedIntNDecimal":
        nPattern = /(^[+]?0(\.0+)?$)|(^([-+]?[1-9][0-9]*)$)|(^([-+]?((0?|[1-9][0-9]*)\.(0*[1-9][0-9]*)))$)|(^[-+]?[1-9]+[0-9]*\.0+$)/;
        spanText += " 需输入十进制数（非0整数首位不可以是0）";// "Only signed integer and decimal value allowed";
        break;
//    case "IsSignedFloatNDecimal":
//        nPattern = /^([-+]?[0-9]*\.?[0-9]+)$/;
//        spanText += " 需输入带符号小数";// "Only signed float and decimal value allowed";
//        break;
    case "IsValidEmail":
        //nPattern = /^(\".*\"|[A-Za-z]\w*)@(\[\d{1,3}(\.\d{1,3}){3}]|[A-Za-z]\w*(\.[A-Za-z]\w*)+)$/;
        nPattern = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/ ;
        spanText += " 需输入正确邮箱地址";// "Please enter valid email id";
        break;
    case "IsValidURL":
        nPattern = "^[A-Za-z]+://[A-Za-z0-9-_]+\\.[A-Za-z0-9-_%&\?\/.=]+$";
        spanText += " 需输入正确链接地址";// "Please enter valid URL";
        break;
    case "IsValidDate":
        nPattern = /^(?=\d)(?:(?:(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2}))($|\ (?=\d)))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ [AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
        spanText += " 需输入正确日期";// "Please enter valid date";
        break;
    }
    matchVal = valCnt.match(nPattern);
    if (matchVal == null)
    {
        return false;
    }
    return true;
 }

//null
function ValidationMessage(valStr,v)
{
    if(valStr!="")
    {
        valStr = valStr + "\n" + v;
    }
    else
    {
        valStr = v; 
    }
    return valStr;
}
/*zczj*/
/*Huang*/
//**********键盘上控制输入Table的←↑→↓
    var   cols=29; 
    var   obj; 
    var   CanMove=false; 
    var   key; 
    function  setobj(input)
    { 
        obj=input; 
    } 
    function  init()
    { 
        document.onkeydown=keyDown; 
        document.onkeyup=keyUp; 
    } 
    function   keyDown(DnEvents)
    { 
        var   key=window.event.keyCode; 
//        if(key==8)       //Backspace
//        { 
//            if(event.srcElement.tagName!= "INPUT ")
//            { 
//                event.cancelBubble   =   true; 
//                event.returnValue   =   false; 
//                return   false; 
//             } 
//        } 
        for(var   i=0;i <document.forms[0].elements.length;i++)
        { 
            if(document.forms[0].elements[i]==obj)
            { 
//                if(event.keyCode==13)   //ENTER 
//                { 
//                    if(i <document.forms[0].elements.length-1)
//                    { 
//                        document.forms[0].elements[i+1].focus(); 
//                    } 
//                } 
                if   (key   ==   37)     //← 
                {
                    if(i> 0)
                    { 
                        document.forms[0].elements[i-1].focus(); 
                    } 
                } 
                if   (key   ==   38)      //↑
                { 
                    if(i> cols-1)
                    { 
                        document.forms[0].elements[i-cols].focus(); 
                    } 
                } 
                if   (key   ==   39)       //→
                {
                    if(i <document.forms[0].elements.length-1)
                    { 
                        document.forms[0].elements[i+1].focus(); 
                    } 
                } 
                if   (key   ==   40)       //↓ 
                {
                    if(i <document.forms[0].elements.length-cols)
                    { 
                        document.forms[0].elements[i+cols].focus(); 
                    } 
                } 
            } 
        } 
    } 

    function   keyUp(UpEvents)
    { 
        return   false; 
    } 
 //**********键盘上控制输入Table的←↑→↓  
 
  
  //**********GridView行背景色交替、鼠标划过行变色，点击行变色选中
       function GridViewColor(ctl00_PrimaryContent_GridView1, NormalColor, AlterColor, HoverColor, SelectColor)
        {
            //获取所有要控制的行
            var AllRows = document.getElementById(ctl00_PrimaryContent_GridView1).getElementsByTagName("tr");
            //设置每一行的背景色和事件，循环从1开始而非0，可以避开表头那一行
            for(var i=1; i<AllRows.length; i++)
            {
                //设定本行默认的背景色
                AllRows[i].style.background = i%2==0?NormalColor:AlterColor;
                //如果指定了鼠标指向的背景色，则添加onmouseover/onmouseout事件
                //处于选中状态的行发生这两个事件时不改变颜色
                if(HoverColor != "")
                {
                    AllRows[i].onmouseover = function(){if(!this.selected)this.style.background = HoverColor;}
                    if(i%2 == 0)
                    {
                        AllRows[i].onmouseout = function(){if(!this.selected)this.style.background = NormalColor;}
                    }
                    else
                    {
                        AllRows[i].onmouseout = function(){if(!this.selected)this.style.background = AlterColor;}
                    }
                }
                //如果指定了鼠标点击的背景色，则添加onclick事件
                //在事件响应中修改被点击行的选中状态
                if(SelectColor != "")
                {
                    AllRows[i].onclick = function()
                    {
                        this.style.background = this.style.background==SelectColor?HoverColor:SelectColor;
                        this.selected = !this.selected;
                    }
                }
            }
        }
//**********asp.net的TextBox回车触发事件**********
    function EnterTextBox()
    {
         if(event.keyCode == 13)
           {
             //document.all["<%=btnlink.ClientID%>"].click();
             document.all("ctl00_PrimaryContent_btnlink").click();        
         }
    }
////*********确定勾选材料，生成采购材料计划***********
//    function Confirm()
//    {
//        var obj=document.getElementsByTagName("input");
//        var objstr = '';
//        for(var i=0;i<obj.length;i++)
//        {
//            if(obj[i].type.toLowerCase()=="checkbox" && obj[i].value!="")
//            {
//               if( obj[i].checked)
//               {    
//                  objstr="checked";
//               }
//            }
//        }
//        if(objstr=="checked")
//        {
//             document.getElementById("ctl00_PrimaryContent_confirmid").value="1" ;
//        }
//        else
//        {
//            alert("请选择材料！");
//        }
//    }

//GridView的显示与隐藏
       function switchGridVidew(img,grv1)
        {
           var grv=document.getElementById(grv1);
           if(grv.style.display=='none')
	        {
	          	grv.style.display='block';
	          	img.src='../Assets/images/bar_down.gif';
		        img.title="隐藏";
	        }
	        else if(grv.style.display=='block')
	        {
		        grv.style.display='none';
		        img.src="../Assets/images/bar_show.gif";
		        img.title="打开";
	        }
        }
        
 //金额小写转换为大写
 function Arabia_to_Chinese(obj){
 Num=obj.value;
 newchar = ""; 
 if(Num==""){
 alert("金额不能为空");
 return newchar;
 }
 for(i=Num.length-1;i>=0;i--)
 {
  Num = Num.replace(",","")//替换tomoney()中的“,”
  Num = Num.replace(" ","")//替换tomoney()中的空格
 }
 //Num = Num.replace("￥","")//替换掉可能出现的￥字符
 if(isNaN(Num)) { //验证输入的字符是否为数字
  alert("请检查小写金额是否正确");
  return newchar;
 }
 //---字符处理完毕，开始转换，转换采用前后两部分分别转换---//
 part = String(Num).split(".");
 //小数点前进行转化
 for(i=part[0].length-1;i>=0;i--){
 if(part[0].length > 10){ alert("位数过大，无法计算");return "";}//若数量超过拾亿单位，提示
  tmpnewchar = ""
  perchar = part[0].charAt(i);
  switch(perchar){
    case "0": tmpnewchar="零" + tmpnewchar ;break;
    case "1": tmpnewchar="壹" + tmpnewchar ;break;
    case "2": tmpnewchar="贰" + tmpnewchar ;break;
    case "3": tmpnewchar="叁" + tmpnewchar ;break;
    case "4": tmpnewchar="肆" + tmpnewchar ;break;
    case "5": tmpnewchar="伍" + tmpnewchar ;break;
    case "6": tmpnewchar="陆" + tmpnewchar ;break;
    case "7": tmpnewchar="柒" + tmpnewchar ;break;
    case "8": tmpnewchar="捌" + tmpnewchar ;break;
    case "9": tmpnewchar="玖" + tmpnewchar ;break;
  }
  switch(part[0].length-i-1){
    case 0: tmpnewchar = tmpnewchar +"元" ;break;
    case 1: if(perchar!=0)tmpnewchar= tmpnewchar +"拾" ;break;
    case 2: if(perchar!=0)tmpnewchar= tmpnewchar +"佰" ;break;
    case 3: if(perchar!=0)tmpnewchar= tmpnewchar +"仟" ;break;
    case 4: tmpnewchar= tmpnewchar +"万" ;break;
    case 5: if(perchar!=0)tmpnewchar= tmpnewchar +"拾" ;break;
    case 6: if(perchar!=0)tmpnewchar= tmpnewchar +"佰" ;break;
    case 7: if(perchar!=0)tmpnewchar= tmpnewchar +"仟" ;break;
    case 8: tmpnewchar= tmpnewchar +"亿" ;break;
    case 9: tmpnewchar= tmpnewchar +"拾" ;break;
  }
  newchar = tmpnewchar + newchar;
 }
 //小数点之后进行转化
 if(Num.indexOf(".")!=-1){
 if(part[1].length > 2) {
  alert("小数点之后只能保留两位,系统将自动截段");
  part[1] = part[1].substr(0,2)
  }
 for(i=0;i<part[1].length;i++){
  tmpnewchar = ""
  perchar = part[1].charAt(i)
  switch(perchar){
    case "0": tmpnewchar="零" + tmpnewchar ;break;
    case "1": tmpnewchar="壹" + tmpnewchar ;break;
    case "2": tmpnewchar="贰" + tmpnewchar ;break;
    case "3": tmpnewchar="叁" + tmpnewchar ;break;
    case "4": tmpnewchar="肆" + tmpnewchar ;break;
    case "5": tmpnewchar="伍" + tmpnewchar ;break;
    case "6": tmpnewchar="陆" + tmpnewchar ;break;
    case "7": tmpnewchar="柒" + tmpnewchar ;break;
    case "8": tmpnewchar="捌" + tmpnewchar ;break;
    case "9": tmpnewchar="玖" + tmpnewchar ;break;
  }
  if(i==0)tmpnewchar =tmpnewchar + "角";
  if(i==1)tmpnewchar = tmpnewchar + "分";
  newchar = newchar + tmpnewchar;
 }
 }
 //替换所有无用汉字
 while(newchar.search("零零") != -1)
  newchar = newchar.replace("零零", "零");
 newchar = newchar.replace("零亿", "亿");
 newchar = newchar.replace("亿万", "亿");
 newchar = newchar.replace("零万", "万");
 newchar = newchar.replace("零元", "元");
 newchar = newchar.replace("零角", "");
 newchar = newchar.replace("零分", "");

 if (newchar.charAt(newchar.length-1) == "元" || newchar.charAt(newchar.length-1) == "角")
  newchar = newchar+"整";
  alert(newchar);
 return newchar;
}

//处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外  

 function banBackSpace(e){  

    var ev = e || window.event;//获取event对象  

    var obj = ev.target || ev.srcElement;//获取事件源  

    var t = obj.type || obj.getAttribute('type');//获取事件源类型  
    //获取作为判断条件的事件类型  

    var vReadOnly = obj.readOnly;  

   var vDisabled = obj.disabled;  

   //处理undefined值情况  

    vReadOnly = (vReadOnly == undefined) ? false : vReadOnly;  

    vDisabled = (vDisabled == undefined) ? true : vDisabled;  

    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，  

    //并且readOnly属性为true或disabled属性为true的，则退格键失效  

    var flag1= ev.keyCode == 8 && (t=="password" || t=="text" || t=="textarea")&& (vReadOnly==true || vDisabled==true);  

   //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效  

    var flag2= ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea" ;  

    //判断  

    if(flag2 || flag1)return false;  

}  

//处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外和F5刷新
function check(e) {  
    var code;  
    if (!e) var e = window.event;  
    if (e.keyCode) code = e.keyCode;  
    else if (e.which) code = e.which;  
    if (((event.keyCode == 8) &&                                                    //BackSpace   
         ((event.srcElement.type != "text" &&   
         event.srcElement.type != "textarea" &&   
         event.srcElement.type != "password") ||   
         event.srcElement.readOnly == true)) ||   
        ((event.ctrlKey) && ((event.keyCode == 78) || (event.keyCode == 82)) ) ||    //CtrlN,CtrlR   
        (event.keyCode == 116) ) {                                                   //F5   
        event.keyCode = 0;   
        event.returnValue = false;   
    }  
    return true;  
}  
//Table的←↑→↓控制
function grControlFocus(input)
{
  var e=event.srcElement;
  var rowIndex=e.parentNode.parentNode.rowIndex ; //获取行号
  var obj=input;  //获得聚焦的文本框
  var tr=e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
  var rowcount=tr.length-1;  //行数
  var td=e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
  var cellcount=td.length-3;   //列数
  var key=window.event.keyCode;   //获得按钮的编号
  var cellIndex;
  for(i=0;i<td.length;i++)
  {
      var objtd=td[i].getElementsByTagName("input")[0];
      if(objtd.id==obj.id)
      {
        td[i-1].getElementsByTagName("input")[0].focus();
        cellIndex=i;
        break;
      }
  }
  if(key==37)   //向左 
  {
      if(rowIndex==1)  //第一行
      {
         if(cellIndex!=2)  
         {
           tr[rowIndex].getElementsByTagName("td")[cellIndex-1].getElementsByTagName("input")[0].focus();
         }
         else   //第一行第二列
         {
            tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
         }
      }
      else
      {
         if(cellIndex!=2)
         {
           tr[rowIndex].getElementsByTagName("td")[cellIndex-1].getElementsByTagName("input")[0].focus();
         }
         else
         {
            tr[rowIndex-1].getElementsByTagName("td")[td.length-3].getElementsByTagName("input")[0].focus();
         }
      }
  }
  if(key==38)  //向上
  {
     if(rowIndex!=1)
     {
        tr[rowIndex-1].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
     }
     else
     {
        tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
     }
  }
  if(key==39)  //向右
  {
     if(rowIndex!=rowcount)
     {
        if(cellIndex!=cellcount)
        {
           tr[rowIndex].getElementsByTagName("td")[cellIndex+1].getElementsByTagName("input")[0].focus();
        }
        else
        {
           tr[rowIndex+1].getElementsByTagName("td")[2].getElementsByTagName("input")[0].focus();
        }
     }
     else
     { 
        if(cellIndex!=cellcount)
        {
           tr[rowIndex].getElementsByTagName("td")[cellIndex+1].getElementsByTagName("input")[0].focus();
        }
        else
        {
           tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
        }
     }
  }
  if(key==40)   //向下
  {
     if(rowIndex!=rowcount)
     {
        tr[rowIndex+1].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
     }
     else
     {
        tr[rowIndex].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].focus();
     }
  }
}




//金额小写转换为大写 ——传字符串
 function Arabia_to_Chinese_UserString(str){
 Num=str;
 newchar = ""; 
 if(Num==""){
 alert("金额不能为空");
 return newchar;
 }
 for(i=Num.length-1;i>=0;i--)
 {
  Num = Num.replace(",","")//替换tomoney()中的“,”
  Num = Num.replace(" ","")//替换tomoney()中的空格
 }
 //Num = Num.replace("￥","")//替换掉可能出现的￥字符
 if(isNaN(Num)) { //验证输入的字符是否为数字
  alert("请检查小写金额是否正确");
  return newchar;
 }
 //---字符处理完毕，开始转换，转换采用前后两部分分别转换---//
 part = String(Num).split(".");
 //小数点前进行转化
 for(i=part[0].length-1;i>=0;i--){
 if(part[0].length > 10){ alert("位数过大，无法计算");return "";}//若数量超过拾亿单位，提示
  tmpnewchar = ""
  perchar = part[0].charAt(i);
  switch(perchar){
    case "0": tmpnewchar="零" + tmpnewchar ;break;
    case "1": tmpnewchar="壹" + tmpnewchar ;break;
    case "2": tmpnewchar="贰" + tmpnewchar ;break;
    case "3": tmpnewchar="叁" + tmpnewchar ;break;
    case "4": tmpnewchar="肆" + tmpnewchar ;break;
    case "5": tmpnewchar="伍" + tmpnewchar ;break;
    case "6": tmpnewchar="陆" + tmpnewchar ;break;
    case "7": tmpnewchar="柒" + tmpnewchar ;break;
    case "8": tmpnewchar="捌" + tmpnewchar ;break;
    case "9": tmpnewchar="玖" + tmpnewchar ;break;
  }
  switch(part[0].length-i-1){
    case 0: tmpnewchar = tmpnewchar +"元" ;break;
    case 1: if(perchar!=0)tmpnewchar= tmpnewchar +"拾" ;break;
    case 2: if(perchar!=0)tmpnewchar= tmpnewchar +"佰" ;break;
    case 3: if(perchar!=0)tmpnewchar= tmpnewchar +"仟" ;break;
    case 4: tmpnewchar= tmpnewchar +"万" ;break;
    case 5: if(perchar!=0)tmpnewchar= tmpnewchar +"拾" ;break;
    case 6: if(perchar!=0)tmpnewchar= tmpnewchar +"佰" ;break;
    case 7: if(perchar!=0)tmpnewchar= tmpnewchar +"仟" ;break;
    case 8: tmpnewchar= tmpnewchar +"亿" ;break;
    case 9: tmpnewchar= tmpnewchar +"拾" ;break;
  }
  newchar = tmpnewchar + newchar;
 }
 //小数点之后进行转化
 if(Num.indexOf(".")!=-1){
 if(part[1].length > 2) {
  alert("小数点之后只能保留两位,系统将自动截段");
  part[1] = part[1].substr(0,2)
  }
 for(i=0;i<part[1].length;i++){
  tmpnewchar = ""
  perchar = part[1].charAt(i)
  switch(perchar){
    case "0": tmpnewchar="零" + tmpnewchar ;break;
    case "1": tmpnewchar="壹" + tmpnewchar ;break;
    case "2": tmpnewchar="贰" + tmpnewchar ;break;
    case "3": tmpnewchar="叁" + tmpnewchar ;break;
    case "4": tmpnewchar="肆" + tmpnewchar ;break;
    case "5": tmpnewchar="伍" + tmpnewchar ;break;
    case "6": tmpnewchar="陆" + tmpnewchar ;break;
    case "7": tmpnewchar="柒" + tmpnewchar ;break;
    case "8": tmpnewchar="捌" + tmpnewchar ;break;
    case "9": tmpnewchar="玖" + tmpnewchar ;break;
  }
  if(i==0)tmpnewchar =tmpnewchar + "角";
  if(i==1)tmpnewchar = tmpnewchar + "分";
  newchar = newchar + tmpnewchar;
 }
 }
 //替换所有无用汉字
 while(newchar.search("零零") != -1)
  newchar = newchar.replace("零零", "零");
 newchar = newchar.replace("零亿", "亿");
 newchar = newchar.replace("亿万", "亿");
 newchar = newchar.replace("零万", "万");
 newchar = newchar.replace("零元", "元");
 newchar = newchar.replace("零角", "");
 newchar = newchar.replace("零分", "");

 if (newchar.charAt(newchar.length-1) == "元" || newchar.charAt(newchar.length-1) == "角")
  newchar = newchar+"整";
  alert(newchar);
 return newchar;
}

//判断金额的格式 是否为最多两位小数的正数
function check_num(obj)
{
var num=obj.value;
var patten=/^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,2})?$/;
if(!patten.test(num))
{ alert('请输入正确的数据格式！！！');
obj.value="0";
obj.focus();
}
}

//单击行变色，单击其他行还原
function RowClick(obj)
{
   var table=obj.parentNode.parentNode;
   var tr=table.getElementsByTagName("tr");
   var ss=tr.length;
   for(var i=1;i<ss-1;i++)
   {
       tr[i].style.backgroundColor=(tr[i].style.backgroundColor == '#87CEFF') ? '#ffffff' : '#ffffff';
   }
   obj.style.backgroundColor=(obj.style.backgroundColor == '#ffffff') ? '#87CEFF' : '#ffffff';
}