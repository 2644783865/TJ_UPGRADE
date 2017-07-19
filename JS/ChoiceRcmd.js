//前台增加代码,放置在GridView中要调用控件处
/*
 <div  class="hidden" style="position:absolute;background-color:#f3f3f3; cursor:hand; border: #B9D3EE 3px solid; padding:0px; margin:0px;">
   <ul   style="list-style-type:square;text-align:left; line-height:normal;"></ul>
</div>
*/

//调用方式:onfocus="findChoice(this,0,28)"



var oInputField;
var oDiv;  
var oChoiceUl;
var aChoice;//待选择项
var prvDiv;//上一个Div
var prvChoiceUl;
var choice_Type;
var choice_Index;

//输入参数:当前对象，类型，列号
function findChoice(obj,choice_type,choice_index)
{
   oInputField=obj;
   choice_Type=choice_type;
   choice_Index=choice_index;
   var tr=obj.parentNode.parentNode;
   var tb=tr.parentNode;
   var rowCounts=tb.rows.length;
   
   if(prvDiv!=null)
   {
     clearPrvChoice();
   }
   
   oDiv =tr.getElementsByTagName("td")[choice_index].getElementsByTagName("div")[0];
   oDiv.style.width="60px";
   oChoiceUl =tr.getElementsByTagName("td")[choice_index].getElementsByTagName("ul")[0];
   
   
   prvDiv=oDiv;
   prvChoiceUl=oChoiceUl;

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
     
      case 1:
         aChoice=["√","×","铸","锻","协 ","非 ","板","型","圆","采","采购成品"];break;
      case 2 :  
         aChoice=["√","×","剪","锯","切","激","割"];break;
      case 3:
          aChoice = ["√", "×", "车", "铣", "镗", "钻", "插", "刨", "磨", "线", "焊", "铆", "折", "卷", "割", "正", "回", "退", "淬", "振", "调", "碳", "氮","喷","堆","电","装","涂","烧","陶","试","包"]; break;
      default:
         aChoice=[""];break;      
   }
}
 

function setChoice(the_choice)
{
  clearChoice();
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
    //用户点击某个匹配项时，设置输入框为该项的值
    if(this.firstChild.nodeValue=="×")
    {
       oInputField.value = "";
    }
    else if(this.firstChild.nodeValue=="√")
    {
          ;
    }
    else
    {
      if(choice_Type==3)
      {
         if(oInputField.value!="")
         {
             oInputField.value +="-"+this.firstChild.nodeValue;
         }
         else
         {
             oInputField.value = this.firstChild.nodeValue;
         }
      }
      else 
      {
        oInputField.value = this.firstChild.nodeValue;
      }
      SettingInMs(choice_Type,choice_Index,this.firstChild.nodeValue);
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
 for(var i=prvChoiceUl.childNodes.length-1;i>=0;i--)
  prvChoiceUl.removeChild(oChoiceUl.childNodes[i]);
  prvDiv.className = "hidden";
}
///制作明细调整时，库或工艺流程列不为空时，体现在制作明细中
function SettingInMs(choice_type,choice_index,obj_value)
{
   var table=document.getElementById(getClientId().Grv);
   var index=oInputField.parentNode.parentNode.rowIndex;
   //如果是库或工艺流程
   if(choice_type==2||choice_type==3)
   {
      if(choice_index==8)
      {
         if(obj_value=="库")
         {
            table.rows[index].cells[7].getElementsByTagName("select")[0].selectedIndex=1;
         }
      }
      else if(choice_index==11)
      {
         if(obj_value!="")
         {
             table.rows[index].cells[7].getElementsByTagName("select")[0].selectedIndex=1;
         }
      }
   }
}


