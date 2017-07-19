var oInputField;
var oDiv;  
var oChoiceUl;
var aChoice;//待选择项
var prvChoiceUl;
var prvDiv;
var choice_Type;


//输入参数:当前对象，类型，列号
function findChoice(obj,choice_type,div_name,ul_name)
{
   oInputField=obj;
   choice_Type=choice_type;   
   oDiv=document.getElementById(div_name);
   oDiv.style.width="60px";
   oChoiceUl=document.getElementById(ul_name);
   
   if(prvDiv!=null)
   {
      clearPrvChoice();
   }
   
   prvChoiceUl=oChoiceUl;
   prvDiv=oDiv;
   
   ChoiceSelecet(choice_type);
   var aResult = new Array();  //用于存放匹配结果
   for(var i=0;i<aChoice.length;i++)
   {
      aResult.push(aChoice[i]);
   }
   setChoice(aResult);
}

function ChoiceSelecet(type)
{
   switch(type)
   {
      case 0://转换率
         aChoice=["×","1","1000"];break;
      case 1://技术单位
         aChoice=["×","kg","kg/m","米","个","套 ","台 ","平米","件","升","把","立方米","卷","支"];break;
      case 2 : //采购主单位
         aChoice=["×","T","kg","个","套","台 ","平米","米","件","升","把","立方米","卷","支"];break;
      case 3:  //采购辅助单位
         aChoice=["×","张","根","支","块","kg","T","个","套","台 ","平米","件","升","把","米","立方米","卷","支"];break;
      default:
         aChoice=[""];break;      
   }
}


function setChoice(the_choice)
{
  clearChoice()
 //显示提示框，传入的参数即为匹配出来的结果组成的数组
 oDiv.className = "show";
 var oLi;
 for(var i=0;i<the_choice.length;i++){
  //将匹配的提示结果逐一显示给用户
  oLi = document.createElement("li");
  oChoiceUl.appendChild(oLi);
  oLi.appendChild(document.createTextNode(the_choice[i]));
  
  oLi.onmouseover = function()
  {
    this.style.background="#004a7e"; //鼠标经过时高亮color:
    this.style.color="#FFFFFF";
  }
  
  oLi.onmouseout = function()
  {
      this.style.background="#f3f3f3"; //离开时恢复原样
      this.style.color="#004a7e";
  }
  
  oLi.onclick = function()
  {
    if(this.firstChild.nodeValue!="×")
    {
       oInputField.value = this.firstChild.nodeValue;
    } 
    clearChoice();
  }
 }
}


function clearChoice()
{
 //清除提示内容
 for(var i=oChoiceUl.childNodes.length-1;i>=0;i--)
  oChoiceUl.removeChild(oChoiceUl.childNodes[i]);
 oDiv.className = "hidden";
}

function clearPrvChoice()
{
 //清除提示内容
// for(var i=prvChoiceUl.childNodes.length-1;i>=0;i--)
//  prvChoiceUl.removeChild(oChoiceUl.childNodes[i]);
  prvDiv.className = "hidden";
}
