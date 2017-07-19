

//Table的←↑→↓控制
function grControlFocus(input)
{
  var e=event.srcElement;
  var rowIndex=e.parentNode.parentNode.rowIndex ; //获取行号
  var cellIndex=e.parentNode.cellIndex;  //获取焦点的列号
  var tr=e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
  var rowcount=tr.length;  //行数
  var td=e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
  var cellcount=td.length;    //列数
  var key=window.event.keyCode;   //获得按钮的编号
 
  if(key==37)   //向左 
  {
  //是否为第一列
      for(var i=cellIndex-1;i>0;i--)
      {
          if( tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null)
          {
              continue;
          }
          else
          {
              tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].select();
              tr[rowIndex].style.backgroundColor ='#55DF55';
              break;
          }
      }
  }
  
  if(key==38)  //向上
  {
//     if(rowIndex!=1)
//     {
//     //不是第一行
     
        for(var i=rowIndex-1;i>0;i--)
        {
           if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null)
            {
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
                tr[rowIndex].style.backgroundColor ='#EFF3FB';
                tr[i].style.backgroundColor ='#55DF55';
                break;
            }
            else
            {
               continue;
            }
        }
//     }
//     else
//     {
//     //是第一行
//          for(var i=tr.length-1;i>0;i--)
//          {
//              if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null)
//              {
//                 tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
//                 tr[rowIndex].style.backgroundColor ='#EFF3FB';
//                 tr[i].style.backgroundColor ='#55DF55';
//                 break;
//              }
//              else
//              {
//                continue;
//              }
//          }
//     }
  }
  
  if(key==39)  //向右
  {
        for(var i=cellIndex+1;i<cellcount;i++)
        {
             if(tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null)
             {
                continue;
             }
             else
             {
               tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].select();
               break;
             }
        } 
   } 
  
  if(key==40)   //向下
  {
     for(var i=rowIndex+1;i<rowcount;i++)
     {
         if(tr[i].getElementsByTagName("td")[cellIndex]!=null)
         {
           if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null)
           {
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
                tr[rowIndex].style.backgroundColor ='#EFF3FB';//原来的行变回原来的颜色
                tr[i].style.backgroundColor ='#55DF55';//下一行变色
                break;
            }
            else
            {
            continue;
            }
         }
         else
         {
           continue;
         }
      }
  }
}

function getSelect(obj)
{
  var objtr=obj.parentNode.parentNode;
  objtr.style.backgroundColor ='#55DF55';

}

