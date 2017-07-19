var conn;
var objrs;
var comText;
var sub_marid;
var tsaid;
var Xishu_B_Shape;
var Xishu_X_Shape;


function BathAutoTuHao(obj)
{
   if(document.getElementById("ckbTuhao").checked)
   {
        if(obj.value!="")
        {
            var table=document.getElementById (getClientId().Id1);
            var tablerows=table.rows.length;
            var tr=table.getElementsByTagName("tr");
            var index=obj.parentNode.parentNode.rowIndex;
            var t=parseInt(index)+1;
            if(t<tablerows)
            {
               if(table.rows[t].cells[2].getElementsByTagName("input")[0].value=="")
               {
                  table.rows[t].cells[2].getElementsByTagName("input")[0].value=obj.value;
               }
            }
        }
    }
}

/*助记码操作*/
function autoMarCode(input)
{
    var marid=document.getElementById(input.id).value;
    var table=document.getElementById(getClientId().Id1);
    var tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    //所有单元格清空(图号，总序，中文名称，备注，关键部件，定尺除外)
    tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="1";//数量默认为1
    tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[1].value=document.getElementById (getClientId().Id2).value;
    tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[2].value=document.getElementById (getClientId().Id2).value;
    tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[30].getElementsByTagName("input")[0].value="";
    
    if(marid!="")
    {
        if(marid.indexOf(" ")>-1)
        {
           sub_marid=marid.substring(0,marid.indexOf(" "));
        }
        else
        {
           sub_marid=marid;
        }
        var child_marid = sub_marid.substring(0,5);
        var son_marid=child_marid.substring(0,2);
        tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=sub_marid;
        connSql(1);//连接数据库获取物料信息  MNAME,GUIGE,CAIZHI,TECHUNIT,MWEIGHT,GB
        if(!objrs.BOF&!objrs.EOF)
        {
           if(child_marid == "01.01")//标准件
           {
            //MNAME,GUIGE,CAIZHI,TECHUNIT,MWEIGHT,GB
            tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//材料规格
            tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//规格
            tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=objrs.Fields(2).Value;//材质
            tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value=objrs.Fields(3).Value;//单位
            tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value=objrs.Fields(5).Value;//国标
            tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value="采";//毛坯
            tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value="标";//状态
            tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;//理论重量
            
            tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;
            tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;;
            tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;;
            tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;;
            
          }
          else
          {
            //RM_NAME,RM_MWEIGHT,RM_GUIGE,RM_CAIZHI,RM_UNIT
            tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//材料规格
            tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//规格
            tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=objrs.Fields(2).Value;//材质
            tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value=objrs.Fields(3).Value;//单位
            tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;//理论重量
            tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value=objrs.Fields(5).Value;//国标

            if(objrs.Fields(0).Value.indexOf("钢板")>-1||objrs.Fields(0).Value=="钢格板"||objrs.Fields(0).Value=="栅格板"||objrs.Fields(0).Value.indexOf("花纹板")>-1)
            {
                tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value="板";//毛坯
                if(objrs.Fields(0).Value=="钢格板")
                {
                    tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value="采";//毛坯
                    tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value="成交";//状态
                }
            }
            else if(objrs.Fields(0).Value.indexOf("圆钢")>-1)
            {
                tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value="圆钢";//毛坯
            }
            else if(objrs.Fields(0).Value.indexOf("型钢")>-1||objrs.Fields(0).Value=="扁钢"||objrs.Fields(0).Value.indexOf("焊管")>-1||objrs.Fields(0).Value=="无缝管"||objrs.Fields(0).Value.indexOf("槽钢")>-1||objrs.Fields(0).Value.indexOf("角钢")>-1||objrs.Fields(0).Value=="工字钢"||objrs.Fields(0).Value=="方钢"||objrs.Fields(0).Value=="方钢管"||objrs.Fields(0).Value=="矩形管"||objrs.Fields(0).Value.indexOf("轨道")>-1||objrs.Fields(0).Value.indexOf("管")>-1)
            {
                tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value="型";//毛坯
            }
          }
          if(tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=="")
          {
              alert("无法识别物料毛坯，请输入！！！");
              tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].focus();
          }
      }
      else
      {
         if(sub_marid!="")
         {
           alert("提示:物料编码【"+sub_marid+"】格式不正确或记录不存在！！！");
           document.getElementById(input.id).value="";
         }
      }
   }
}



    //长宽改变
    function autoLW(input)
    {
        var table=document.getElementById(getClientId().Id1);
        var tr=table.getElementsByTagName("tr");
        var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
        var cailiaocd=tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
        var cailiaokd=tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度
        var shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");//总数量
        var p_shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");//计划数量
        var dw=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value;//技术单位-采购单位
        var fix=tr[index].getElementsByTagName("td")[32].getElementsByTagName("select")[0].value;
        GetXiShu_TMBluk(fix);
        
        var caigoudw="";
        if(dw.indexOf(")-(")>-1)
        {
          caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
        }
        else
        {
          caigoudw="";
        }
        var pattem=/^\d+(\.\d+)?$/;//数量验证
        var pattem2=/^[0-9]*$/;//长、宽验证
        
        if(cailiaocd=="")
        {
           cailiaocd="0";
           tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
        }
        
        if(cailiaokd=="")
        {
           cailiaokd="0";
           tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
        }
        
        if(shuliang=="")
        {
           shuliang=document.getElementById (getClientId().Id2).value;
           tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="1";
           tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[1].value=shuliang;
           tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[2].value=shuliang;
        }
        
        //材料长度格式检查
        if(pattem2.test(cailiaocd))
        {
           if(parseInt(cailiaocd)>10000)
           {
              alert('提示:输入【材料长度】超出10米,请核实！！!');
           }
        }
        else
        {
            alert('提示:输入【材料长度】格式不正确!');
            tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
            return false;
        }
        //材料宽度检查
        if(pattem2.test(cailiaokd))
        {
           if(parseInt(cailiaokd)>10000)
           {
              alert('提示:输入【材料宽度】超出10米,请核实！！!');
           }
        }
        else
        {
            alert('提示:输入【材料宽度】格式不正确!');
            tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
            return false;
        }
        
        //数量检查
        if(pattem.test(shuliang))
        {
           if(parseInt(shuliang)>50)
           {
              if(input.parentNode.cellIndex==9)
              {
                 alert('提示:输入【数量】超出50,请核实！！!');
              }  
           }
        }
        else
        {
            alert('提示:输入【数量】格式不正确!');
            tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="1";
            return false;
        }
        //格式检查完毕，开始处理数据
        var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var cailiaoname=tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
        var dzh=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
        if(marid!="")
        {
           var cailiaodzh="0";
           var cailiaozongzhong="0";
           var bgzmy="0";
           var guige=tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
           var cailiaoguige=tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
           var lilunzhl=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
           var zongzhong="0";
           var cailiaozongchang=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
           var sub_marid=marid.substring(0, 5);
           var child_sub_marid = sub_marid.substring(0, 2);
           var mapishape=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value;
           if(sub_marid!="01.01") //非标准件
           {
                    if (mapishape=="板"||cailiaoname.indexOf("钢板")>-1||cailiaoname.indexOf("栅格板")>-1 || cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("钢板网")>-1) //
                    {
                        if (cailiaocd != 0 && cailiaokd != 0)
                        {
                            guige = cailiaoguige + 'x' + cailiaokd + '+' + cailiaocd;
                            if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢板网")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1)
                            {
                               cailiaodzh = (cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                               if(cailiaoname.indexOf("钢格板")>-1)
                               {
                                 Xishu_B_Shape="1";
                               }
                            }
                            else
                            {
                               cailiaodzh = (cailiaoguige * cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                            }
                            cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                            
                            //计算实际单重和总重
                            bgzmy=(cailiaocd*cailiaokd/1000000).toFixed(2);//材料形状规则
                            dzh=cailiaodzh;
                            zongzhong=(dzh*shuliang).toFixed(2);
                            //end计算实际单重和总重
                        }
                        else
                        {
                            if(dzh=="")
                            {
                               dzh=0;
                            }
                            guige = cailiaoguige;
                            zongzhong=(dzh*shuliang).toFixed(2);
                            cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                        }
                    }
                    else if(mapishape=="型"||mapishape=="圆钢")//cailiaoname.indexOf("圆钢")>-1||cailiaoname.indexOf("型钢")>-1||cailiaoname=="扁钢"||cailiaoname.indexOf("焊管")>-1||cailiaoname.indexOf("焊接管")>-1||cailiaoname=="无缝管"||cailiaoname=="无缝钢管"||cailiaoname.indexOf("槽钢")>-1||cailiaoname.indexOf("角钢")>-1||cailiaoname=="工字钢"||cailiaoname=="方钢"||cailiaoname=="方钢管"||cailiaoname=="矩形管"||cailiaoname.indexOf("轨道")>-1
                    {
                        if(cailiaocd!=0)
                        {
                            guige = cailiaoguige + '+' + cailiaocd;
                            cailiaodzh = (cailiaocd * lilunzhl / 1000).toFixed(2);
                            cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                            cailiaozongchang = (cailiaocd * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                            dzh=cailiaodzh;
                            zongzhong=(dzh*shuliang).toFixed(2);
                        }
                        else
                        {
                            guige = cailiaoguige;
                        }
                    }
                    else if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                    {
                        guige = cailiaoguige;
                        cailiaozongchang=cailiaocd*p_shuliang;  
                        dzh =tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//实际单重
                        cailiaodzh=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//材料单重
                        cailiaozongzhong=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*p_shuliang*cailiaocd/1000;
                        zongzhong=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*shuliang*cailiaocd/1000;
                    }
                    else
                    {
                        guige = cailiaoguige;
                        cailiaodzh = (lilunzhl*1).toFixed(2);
                        cailiaozongzhong = (cailiaodzh * p_shuliang).toFixed(2);
                        cailiaozongchang = (cailiaocd * p_shuliang).toFixed(2);
                        dzh=cailiaodzh;
                        zongzhong=(dzh*shuliang).toFixed(2);
                    }
                    
                    tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=dzh;
                    tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=cailiaodzh;
                    tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=cailiaozongzhong;
                    tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=bgzmy;
                    tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value=guige;
                    tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=zongzhong;
                    tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=cailiaozongchang;
                    
                    if(tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=="")//图纸上单重
                    {
                       tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
                    }   
                }
                else if(marid.indexOf("01.01")>-1)
                {
                        if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                        {
                            tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=cailiaocd*p_shuliang;  
                            tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//实际单重
                            tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//材料单重
                            tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*p_shuliang*cailiaocd/1000;
                            tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*shuliang*cailiaocd/1000;
                        }
                        else
                        {
                            tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;//实际单重
                            tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;//材料单重
                            tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*p_shuliang;
                            tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*shuliang;
                        }
                }
                
                //采购单位为平米时计算面域
                if(caigoudw.indexOf("(平米")>-1||caigoudw.indexOf("(平方米")>-1||caigoudw.indexOf("(m2")>-1||caigoudw.indexOf("(M2")>-1||caigoudw.indexOf("(㎡")>-1)
                {
                    tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[1].value=(cailiaocd*cailiaokd/1000000).toFixed(2);
                }
        }
        else
        {
           if(input.parentNode.cellIndex!=9)//物料编码为空时，输入长宽无效
           {
              if(parseInt(cailiaocd)>0||parseInt(cailiaokd)>0)
              {
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
                alert("提示:物料编码为空，输入【材料长度】【材料宽度】无效！！！");
              }
           }
           else//如果输入数量，要修改实际总重
           {
               if(dzh!="")
               {
                   tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=dzh*shuliang;
               }
           }
        }
    }
    
    
        //数量改变
        function autoNum(input)
        {
            var table=document.getElementById(getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var cailiaocd=tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
            var cailiaokd=tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度
            var shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//数量
            var dw=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value;//技术单位-采购单位
            var fix=tr[index].getElementsByTagName("td")[32].getElementsByTagName("select")[0].value;
            GetXiShu_TMBluk(fix);
            var caigoudw="";
            var number=1;
            if(document.getElementById(getClientId().Id2)!=null)
            {
               number=document.getElementById(getClientId().Id2).value;
            }

            if(dw.indexOf(")-(")>-1)
            {
              caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
            }
            else
            {
              caigoudw="";
            }
            var pattem=/^\d+(\.\d+)?$/;//数量验证
            var pattem2=/^[0-9]*$/;//长、宽验证
            
            if(cailiaocd=="")
            {
               cailiaocd="0";
               tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
            }
            
            if(cailiaokd=="")
            {
               cailiaokd="0";
               tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
            }
            
            if(shuliang=="")
            {
               shuliang=number;
               tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="1";
               tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[1].value=number;
               tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[2].value=number;
            }
            else
            {
                shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//数量
                shuliang=shuliang*number;
                tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[1].value=shuliang;
                tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[2].value=shuliang;
            }
            
            //材料长度格式检查
            if(pattem2.test(cailiaocd))
            {
               if(parseInt(cailiaocd)>100000)
               {
                  alert('提示:输入【材料长度】超出100米,请核实！！!');
               }
            }
            else
            {
                alert('提示:输入【材料长度】格式不正确!');
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
                return false;
            }
            //材料宽度检查
            if(pattem2.test(cailiaokd))
            {
               if(parseInt(cailiaokd)>100000)
               {
                  alert('提示:输入【材料宽度】超出100米,请核实！！!');
               }
            }
            else
            {
                alert('提示:输入【材料宽度】格式不正确!');
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
                return false;
            }
            //数量检查
            if(pattem.test(shuliang))
            {
               if(parseInt(shuliang)>50)
               {
                  if(input.parentNode.cellIndex==9)
                  {
                     alert('提示:输入【数量】超出50,请核实！！!');
                  }  
               }
            }
            else
            {
                alert('提示:输入【数量】格式不正确!');
                tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="1";
                return false;
            }
            //格式检查完毕，开始处理数据
            var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaoname=tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var dzh=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
            if(marid!="")
            {
               var cailiaodzh=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
               var cailiaozongzhong=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var bgzmy=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var guige=tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
               var cailiaoguige=tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var lilunzhl=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var zongzhong=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var cailiaozongchang=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var sub_marid=marid.substring(0, 5);
               var child_sub_marid = sub_marid.substring(0, 2);
               var mapishape=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value;
               if(child_sub_marid != "02"||child_sub_marid == "02") //去掉了非低值易耗品限制
               {
                        if(marid.indexOf("01.01")>-1)//理论重量*数量
                        {
                            if((cailiaozongchang!=0&&cailiaozongchang!="")||caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                            {
                                cailiaozongchang=cailiaocd*shuliang;
                                if(cailiaodzh==0)  
                                {
                                   cailiaodzh=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*lilunzhl*cailiaocd/1000;
                                }
                                
                                dzh=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;

                                cailiaozongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                                zongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                            }
                            else
                            {
                                cailiaodzh=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value;
                                dzh=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value;
                                cailiaozongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                                zongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                            } 
                        }
                       else if (mapishape=="板"||cailiaoname.indexOf("钢板")>-1 || cailiaoname.indexOf("花纹板")>-1|| cailiaoname.indexOf("栅格板")>-1||cailiaoname.indexOf("钢格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )//
                       {
                            ////A
                            if (cailiaocd != 0 && cailiaokd != 0)
                            {
                                guige = cailiaoguige + 'x' + cailiaokd + '+' + cailiaocd;
                                if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )
                                {
                                   if(cailiaodzh==0)
                                   {
                                      cailiaodzh = (cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                                   }
                                   if(cailiaoname.indexOf("钢格板")>-1)
                                   {
                                     Xishu_B_Shape="1";
                                   }
                                }
                                else 
                                {
                                   if(cailiaodzh==0)
                                   {
                                     cailiaodzh = (cailiaoguige * cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                                   }  
                                }
                                cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                
                                //计算实际单重和总重
                                bgzmy=(cailiaocd*cailiaokd/1000000).toFixed(2);//材料形状规则
                                if(dzh==0||dzh=="")//单重为0或空
                                {
                                    dzh=cailiaodzh;
                                }
                                zongzhong=(dzh*shuliang).toFixed(2);
                                //end计算实际单重和总重
                            }
                            else
                            {
                                if(dzh=="")
                                {
                                   dzh=0;
                                }
                                guige = cailiaoguige;
                                zongzhong=(dzh*shuliang).toFixed(2);
                                cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                            }
                            ///END A
                        }
                        else if(mapishape=="型"||mapishape=="圆钢")//cailiaoname.indexOf("圆钢")>-1||cailiaoname.indexOf("型钢")>-1||cailiaoname=="扁钢"||cailiaoname.indexOf("焊管")>-1||cailiaoname.indexOf("焊接管")>-1||cailiaoname=="无缝钢管"||cailiaoname=="无缝管"||cailiaoname.indexOf("槽钢")>-1||cailiaoname.indexOf("角钢")>-1||cailiaoname=="工字钢"||cailiaoname=="方钢"||cailiaoname=="方钢管"||cailiaoname=="矩形管"||cailiaoname.indexOf("轨道")>-1
                        {
                            if(cailiaocd!=0)
                            {
                                guige = cailiaoguige + '+' + cailiaocd;
                                if(cailiaodzh==0)
                                {
                                   cailiaodzh = (cailiaocd * lilunzhl / 1000).toFixed(2);
                                }
                                cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                                cailiaozongchang = (cailiaocd * shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                                dzh=cailiaodzh;
                                zongzhong=(dzh*shuliang).toFixed(2);
                            }
                            else
                            {
                                guige = cailiaoguige;
                                
                            }
                        }
                        else if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                        {
                            guige = cailiaoguige;
                            cailiaozongchang=cailiaocd*shuliang;  
                            if(dzh==0)
                            {
                               dzh =tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//实际单重
                            }
                            if(cailiaodzh==0||cailiaodzh=="")   
                            {
                              cailiaodzh=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//材料单重
                            }
                            cailiaozongzhong=(cailiaodzh*shuliang).toFixed(2);
                            zongzhong=(dzh*shuliang).toFixed(2);
                        }
                        else
                        {
                            guige = cailiaoguige;
                            if(cailiaodzh==0)
                            {
                               cailiaodzh = (lilunzhl*1).toFixed(2);
                            }
                            cailiaozongzhong = (cailiaodzh * shuliang).toFixed(2);
                            cailiaozongchang = (cailiaocd * shuliang).toFixed(2);
                            if(dzh==0)
                            {
                              dzh=cailiaodzh;
                            }
                            zongzhong=(dzh*shuliang).toFixed(2);
                        }
                        tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=cailiaozongzhong;
                        tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=zongzhong;
                        tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=cailiaozongchang;
                        if(tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=="")//图纸上单重
                        {
                           tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
                        }   
                    }
                }
            else
            {
               if(input.parentNode.cellIndex!=9)//物料编码为空时，输入长宽无效
               {
                  if(parseInt(cailiaocd)>0||parseInt(cailiaokd)>0)
                  {
                    tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
                    tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
                    alert("提示:物料编码为空，输入【材料长度】【材料宽度】无效！！！");
                  }
               }
               else//如果输入数量，要修改实际总重
               {
                   if(dzh!="")
                   {
                       tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=dzh*shuliang;
                   }
               }
            }
        }
        
        //计划数量改变
        function autoP_Num(input)
        {
            var table=document.getElementById(getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var cailiaocd=tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
            var cailiaokd=tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度
            var shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");//总数量
            var p_shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");//计划数量
            var dw=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value;//技术单位-采购单位
            var fix=tr[index].getElementsByTagName("td")[32].getElementsByTagName("select")[0].value;
            GetXiShu_TMBluk(fix);
            var caigoudw="";
            var number=1;
            if(document.getElementById(getClientId().Id2)!=null)
            {
               number=document.getElementById(getClientId().Id2).value;
            }

            if(dw.indexOf(")-(")>-1)
            {
              caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
            }
            else
            {
              caigoudw="";
            }
            var pattem=/^\d+(\.\d+)?$/;//数量验证
            var pattem2=/^[0-9]*$/;//长、宽验证
            
            if(cailiaocd=="")
            {
               cailiaocd="0";
               tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
            }
            
            if(cailiaokd=="")
            {
               cailiaokd="0";
               tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
            }
            
            if(shuliang=="")
            {
               alert("请输入单台数量！！！");
               input.value="";
               return false;
            }
            
            //材料长度格式检查
            if(pattem2.test(cailiaocd))
            {
               if(parseInt(cailiaocd)>100000)
               {
                  alert('提示:输入【材料长度】超出100米,请核实！！!');
               }
            }
            else
            {
                alert('提示:输入【材料长度】格式不正确!');
                tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
                return false;
            }
            //材料宽度检查
            if(pattem2.test(cailiaokd))
            {
               if(parseInt(cailiaokd)>100000)
               {
                  alert('提示:输入【材料宽度】超出100米,请核实！！!');
               }
            }
            else
            {
                alert('提示:输入【材料宽度】格式不正确!');
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
                return false;
            }
            //数量检查
            if(pattem.test(shuliang))
            {
               if(parseInt(shuliang)>50)
               {
                  if(input.parentNode.cellIndex==9)
                  {
                     alert('提示:输入【数量】超出50,请核实！！!');
                  }  
               }
            }
            
            if(!pattem.test(p_shuliang))
            {
               alert("请输入正确的数值！！！");
               autoNum(tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0]);
               return false;
            }
            
            if(shuliang>p_shuliang)
            {
               alert("计划数量不能小于总数量！！！");
               autoNum(tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[0]);
               return false;
            }
            
            //格式检查完毕，开始处理数据
            var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaoname=tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var dzh=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
            if(marid!="")
            {
               var cailiaodzh=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
               var cailiaozongzhong=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var bgzmy=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var guige=tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
               var cailiaoguige=tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var lilunzhl=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var zongzhong=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var cailiaozongchang=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var sub_marid=marid.substring(0, 5);
               var child_sub_marid = sub_marid.substring(0, 2);
               var mapishape=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value;
               if(child_sub_marid != "02"||child_sub_marid == "02") //非低值易耗品02和标准件
               {
                        if(marid.indexOf("01.01")>-1)//理论重量*数量
                        {
                            if((cailiaozongchang!=0&&cailiaozongchang!="")||caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                            {
                                cailiaozongchang=cailiaocd*p_shuliang;  
                                cailiaodzh=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*lilunzhl*cailiaocd/1000;
                                dzh=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;
                                cailiaozongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*p_shuliang).toFixed(2);
                                zongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                            }
                            else
                            {
                                cailiaodzh=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*lilunzhl*cailiaocd/1000;
                                dzh=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value;
                                cailiaozongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*p_shuliang).toFixed(2);
                                zongzhong=(tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                            } 
                        }
                       else if (mapishape=="板"||cailiaoname.indexOf("钢板")>-1 || cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1 || cailiaoname.indexOf("钢板网")>-1 )//cailiaoname.indexOf("钢板")>-1 || cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1 
                       {
                            ////A
                            if (cailiaocd != 0 && cailiaokd != 0)
                            {
                                guige = cailiaoguige + 'x' + cailiaokd + '+' + cailiaocd;
                                if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )
                                {
                                   cailiaodzh = (cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                                   if(cailiaoname.indexOf("钢格板")>-1)
                                   {
                                      Xishu_B_Shape="1";
                                   }
                                }
                                else 
                                {
                                   cailiaodzh = (cailiaoguige * cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                                }
                                cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                
                                //计算实际单重和总重
                                bgzmy=(cailiaocd*cailiaokd/1000000).toFixed(2);//材料形状规则
                                if(dzh==0||dzh=="")//单重为0或空
                                {
                                    dzh=cailiaodzh;
                                }
                                zongzhong=(dzh*shuliang).toFixed(2);
                                //end计算实际单重和总重
                            }
                            else
                            {
                                if(dzh=="")
                                {
                                   dzh=0;
                                }
                                guige = cailiaoguige;
                                zongzhong=(dzh*shuliang).toFixed(2);
                                cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                            }
                            ///END A
                        }
                        else if(mapishape=="型"||mapishape=="圆钢")//cailiaoname.indexOf("圆钢")>-1||cailiaoname.indexOf("型钢")>-1||cailiaoname=="扁钢"||cailiaoname.indexOf("焊管")>-1||cailiaoname.indexOf("焊接管")>-1||cailiaoname=="无缝钢管"||cailiaoname=="无缝管"||cailiaoname.indexOf("槽钢")>-1||cailiaoname.indexOf("角钢")>-1||cailiaoname=="工字钢"||cailiaoname=="方钢"||cailiaoname=="方钢管"||cailiaoname=="矩形管"||cailiaoname.indexOf("轨道")>-1
                        {
                            if(cailiaocd!=0)
                            {
                                guige = cailiaoguige + '+' + cailiaocd;
                                cailiaodzh = (cailiaocd * lilunzhl / 1000).toFixed(2);
                                cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                                cailiaozongchang = (cailiaocd * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                                dzh=cailiaodzh;
                                zongzhong=(dzh*shuliang).toFixed(2);
                            }
                            else
                            {
                                guige = cailiaoguige;
                                
                            }
                        }
                        else if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                        {
                            guige = cailiaoguige;
                            cailiaozongchang=cailiaocd*p_shuliang;  
                            dzh =tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//实际单重
                            cailiaodzh=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*cailiaocd/1000;//材料单重
                            cailiaozongzhong=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*p_shuliang*cailiaocd/1000;
                            zongzhong=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value*shuliang*cailiaocd/1000;
                        }
                        else
                        {
                            guige = cailiaoguige;
                            cailiaodzh = (lilunzhl*1).toFixed(2);
                            cailiaozongzhong = (cailiaodzh * shuliang).toFixed(2);
                            cailiaozongchang = (cailiaocd * shuliang).toFixed(2);
                            dzh=cailiaodzh;
                            zongzhong=(dzh*shuliang).toFixed(2);
                        }
                        tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=cailiaozongzhong;
                        tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=zongzhong;
                        tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=cailiaozongchang;
                        if(tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=="")//图纸上单重
                        {
                           tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
                        }   
                    }
                }
            else
            {
               if(input.parentNode.cellIndex!=9)//物料编码为空时，输入长宽无效
               {
                  if(parseInt(cailiaocd)>0||parseInt(cailiaokd)>0)
                  {
                    tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
                    tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="0";
                    alert("提示:物料编码为空，输入【材料长度】【材料宽度】无效！！！");
                  }
               }
               else//如果输入数量，要修改实际总重
               {
                   if(dzh!="")
                   {
                       tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=dzh*shuliang;
                   }
               }
            }
        }
        
        //实际单重改变
        function autoRealUnitW(input)
        {
            var table=document.getElementById(getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var dzh=document.getElementById(input.id).value;
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[1].value;
            var cailiaoname=tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaodz=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;//材料单重
            var shape=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//毛坯形状
            var llzl=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//理论重量
            var guige=tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //材料规格
            var pattem=/^\d+(\.\d+)?$/;//实际单重
            var pattem2=/^[1-9][0-9]*$/;//数量验证
            //实际单重为空不计算
            if(dzh=="")
            {
               return false;
            }
            //数量为空默认1
            if(shuliang==""||shuliang==0)
            {
               shuliang="1";
               input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="1";
            }
            
            if(pattem.test(dzh)&&pattem.test(shuliang))
            {
               //计算总重
               input.parentNode.parentNode.getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=(dzh*shuliang).toFixed(2);
               //比较实际单重和材料单重
               if(parseFloat(cailiaodz)<parseFloat(dzh))
               {
                   alert("实际单重超出材料单重，请核实！！！");
               }
               else
               {
                 if(dzh>10000)
                 {
                  alert('提示:输入【实际单重】超出10吨,请核实！！!');
                 }
               } 
               //计算面域（对于板材）
               if((shape=="板"&&pattem.test(guige))||cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )
               {
                   var bgzmy=0;
                   if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )
                   {
                       bgzmy=(dzh/llzl).toFixed(2);
                   }
                   else
                   {
                       bgzmy=(dzh/(llzl*guige)).toFixed(2);
                   }
                   tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=bgzmy;
               } 
               
            }
            else
            {
                alert('提示:输入【实际单重】或【数量】格式不正确!');
                document.getElementById(input.id).value=0;
                document.getElementById(input.id).focus();
            }
            
        }
        
        
        //材料单重改变
        function autoMarUnitW(input)
        {
            var table=document.getElementById(getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cldzh=document.getElementById(input.id).value;
            var lilunzhl=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var shuliang=input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");
            var dzh=input.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
            var bgmy=input.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaoguige=tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var blankshape=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var fix=tr[index].getElementsByTagName("td")[32].getElementsByTagName("select")[0].value;
            GetXiShu_TMBluk(fix);
            
            var pattem=/^\d+(\.\d+)?$/;//材料单重
            var pattem2=/^[1-9][0-9]*$/;//数量验证
            if(cldzh=="")
            {
               return false;
            }
            else
            {
               if(marid=="")
               {
                  alert("提示:物料编码为空，部件输入【材料单重】无效！！！");
                  document.getElementById(input.id).value="0";
                  return false;
               }
            }

            if(shuliang==""||shuliang=="0")
            {
               shuliang=document.getElementById(getClientId().Id2).value;
               input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="1";
               input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[1].value=shuliang;
               input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[2].value=shuliang;
            }
            
            if(pattem.test(cldzh)&&pattem.test(shuliang))
            {
                if(cldzh!=0)
                {
                    //材料总重
                    if(blankshape.indexOf("板")>-1)
                    {
                        input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=(cldzh*shuliang*parseFloat(Xishu_B_Shape)).toFixed(2);
                    }
                    else if(blankshape.indexOf("型")>-1||blankshape=="圆钢")
                    {
                        input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
                        //材料长度
                        input.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=(cldzh*1000/lilunzhl).toFixed(0);
                        //材料总长
                        input.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=(cldzh*shuliang*parseFloat(Xishu_X_Shape)*1000/lilunzhl).toFixed(0);
                    }
                    else
                    {
                        input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
                    }
                    
                    if(dzh==""||dzh==0)//如果实际单重为空或0，重新计算
                    {
                       //实际单重
                       input.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=cldzh;
                       //总重
                       input.parentNode.parentNode.getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
                    }
                    if(cldzh>10000)
                    {
                       alert('提示:输入【材料单重】超出10吨,请核实！！!');
                    }   
                }
                
            }
            else
            {
                alert('提示:输入【材料单重】或【数量】格式不正确!');
                document.getElementById(input.id).value="0";
                document.getElementById(input.id).focus();
            }
        }
        
        
        //面域改变
        function autoMyShpace(input)
        {
            var shuliang=input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[1].value;
            var p_shuliang=input.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[2].value;
            var my=document.getElementById(input.id).value;
            var table=document.getElementById(getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");

            var cailiaocd=tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
            var cailiaokd=tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度

            var cailiaoguige=tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var lilunzhl=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");

            var miany=cailiaocd*cailiaokd/1000000;
            
            var marshape=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//毛坯形状
            var fix=tr[index].getElementsByTagName("td")[32].getElementsByTagName("select")[0].value;
            GetXiShu_TMBluk(fix);
            
            var pattem=/^\d+(\.\d+)?$/;//面域
            var pattem2=/^[1-9][0-9]*$/;//数量验证
            if(!pattem.test(my))
            {
               alert("请输入正确的面域数值！！！")
               input.value="";
               return false;
            }
            
            if(my==""||my=="0")
            {
               return false;
            }
            else
            {
               if(marid=="")
               {
                  alert("提示:物料编码为空，部件输入【面域】无效！！！");
                  document.getElementById(input.id).value="0";
                  return false;
               }
            }
            //数量赋值
            if(shuliang==""||shuliang==0)
            {
                alert("请输入数量！！！");
                input.value="";
                return false;
            }
            
            if(pattem.test(my)&&pattem.test(shuliang))
            {
               if(parseFloat(my)>parseFloat(miany)&marshape=="板"&cailiaocd!="0"&cailiaokd!="0"&cailiaocd!=""&cailiaokd!="")
               {
                  alert("该条物料毛坯形状为【板】，输入面域超出规则长、宽下面域，输入无效！！！");
                  document.getElementById(input.id).value=miany;
                  return false;
               }
               
               if(parseFloat(my)>100)
               {
                  alert("提示:输入【面域】大于100平方米，请核实！！！");
               }
               //格式检查完毕，开始处理数据
                var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
                var cailiaoname=tr[index].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
                if(marid!="")//start marid
                {
                               var dzh=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
                               var cailiaodzh="0";
                               var cailiaozongzhong="0";
                               var bgzmy=my;//单个面域
                               var guige=tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
                               var cailiaoguige=tr[index].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                               var lilunzhl=tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                               var zongzhong="0";
                               var cailiaozongchang=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                               var sub_marid=marid.substring(0, 5);
                               var child_sub_marid = sub_marid.substring(0, 2);
                               var mapishape=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value;
                               if(sub_marid != "01.01") //STR非低值易耗品02和标准件
                               {
                                   //START A
                                   if (mapishape=="板"||cailiaoname.indexOf("钢板")>-1 || cailiaoname.indexOf("钢格板")>-1|| cailiaoname.indexOf("花纹板")>-1 || cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )//
                                   {
                                     //START B 
                                     
                                     if(bgzmy==0)
                                     {
                                        if (cailiaocd != 0 && cailiaokd != 0)
                                        {
                                            guige = cailiaoguige + 'x' + cailiaokd + '+' + cailiaocd;
                                            if(cailiaoname.indexOf("花纹板")>-1|| cailiaoname.indexOf("钢格板")>-1 || cailiaoname.indexOf("栅格板")>-1 || cailiaoname.indexOf("钢板网")>-1 )
                                            {
                                               cailiaodzh = (cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                                               if(cailiaoname.indexOf("钢格板")>-1)
                                               {
                                                 Xishu_B_Shape="1";
                                               }
                                            }
                                            else
                                            {
                                               cailiaodzh = (cailiaoguige * cailiaocd * cailiaokd * lilunzhl / 1000000).toFixed(2);
                                            }
                                            cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                            
                                            //计算实际单重和总重
                                            bgzmy=cailiaocd*cailiaokd/1000000;//材料形状规则
                                            if(dzh==0||dzh=="")//单重为0或空
                                            {
                                                dzh=cailiaodzh;
                                            }
                                            zongzhong=(dzh*shuliang).toFixed(2);
                                            //end计算实际单重和总重
                                        }
                                        else
                                        {
                                            if(dzh=="")
                                            {
                                               dzh=0;
                                            }
                                            guige = cailiaoguige;
                                            zongzhong=(dzh*shuliang).toFixed(2);
                                            cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                        }
                                     }
                                     else //面域不为零时，根据面域计算重量
                                     {
                                            guige = cailiaoguige + 'x' + cailiaokd + '+' + cailiaocd;
                                            
                                            if(cailiaodzh=0||cailiaodzh=="")
                                            {
                                              if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )
                                                {
                                                   cailiaodzh = (bgzmy * lilunzhl).toFixed(2);
                                                }
                                                else
                                                {
                                                   cailiaodzh = (cailiaoguige * bgzmy * lilunzhl).toFixed(2);
                                                }
                                               cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                            }
                                            else
                                            {
                                                cailiaodzh = tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
                                                cailiaozongzhong = tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
                                            }
                                            if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("钢板网")>-1 )
                                            {
                                                //计算实际单重和总重
                                                dzh=(bgzmy * lilunzhl).toFixed(2);
                                                zongzhong=(dzh*shuliang).toFixed(2);
                                                //end计算实际单重和总重
                                            }
                                            else
                                            {
                                                //计算实际单重和总重
                                                dzh=(cailiaoguige * bgzmy * lilunzhl).toFixed(2);
                                                zongzhong=(dzh*shuliang).toFixed(2);
                                                //end计算实际单重和总重
                                            }
                                     }
                                    //END B
                                   }
                                   else
                                   {
                                        if(cailiaocd!=0)
                                        {
                                            guige = cailiaoguige + '+' + cailiaocd;
                                            cailiaodzh = (cailiaocd * lilunzhl / 1000).toFixed(2);
                                            cailiaozongzhong = (cailiaodzh * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                                            cailiaozongchang = (cailiaocd * p_shuliang * parseFloat(Xishu_X_Shape)).toFixed(2);
                                            dzh=cailiaodzh;
                                            zongzhong=(dzh*shuliang).toFixed(2);
                                        }
                                        else
                                        {
                                            guige = cailiaoguige;
                                        }
                                  }
                                //END A
                                tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=dzh;
                                tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=cailiaodzh;
                                tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=cailiaozongzhong;
                                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=bgzmy;
                                tr[index].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value=guige;
                                tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=zongzhong;
                                tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=cailiaozongchang;
                                
                                if(tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=="")//图纸上单重
                                {
                                   tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
                                }   
                            }
                    }
                    else//物料编码为空时，输入面域无效
                    {
                       if(input.parentNode.cellIndex!=13)
                       {
                          if(parseInt(my)>0)
                          {
                            tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value="0";
                            alert("提示:物料编码为空，输入【面域】无效！！！");
                          }
                       }
                    }
                    // end marid
            }
            else
            {
                alert("提示:输入【面域】或【数量】格式不正确！！！");
                document.getElementById(input.id).value="0";
            }
        }
        
        
        //图纸上单重计算
        function autoPageW(input)
        {
            var tudz=document.getElementById(input.id).value;
            var pattem=/^\d+(\.\d+)?$/;
            if(!pattem.test(tudz))
            {
               document.getElementById(input.id).value="";
               alert("提示:输入【图纸上单重】格式不正确！！！");
               return;
            }
            CheckBlukUnitWght_TuUnit(input);

        }
        
        function CheckBlukUnitWght_TuUnit(input)
        {
           var table=document.getElementById (getClientId().Id1);
           var tr=table.getElementsByTagName("tr");
           var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
        
           var obj_real=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0];
           var obj_tu=tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0];
           
           var real=obj_real.value;
           var tudz=obj_tu.value;
           
           if(real=="")
           {
              real=0;
           }
           
           if(tudz=="")
           {
             tudz=0;
           }
            
            if((Math.abs(real-tudz)>0.01*tudz))
            {
               obj_real.style.background="yellow";
               obj_tu.style.background="yellow";
            }
            else
            {
               obj_real.style.background="white";
               obj_tu.style.background="white";
            }
        }
        
        
//控制定尺是否可用
function autoFixSize(obj)
{
   var table=document.getElementById (getClientId().Id1);
   var tr=table.getElementsByTagName("tr");
   var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
   var bancai=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value;
   var fixsize=tr[index].getElementsByTagName("td")[32].getElementsByTagName("select")[0].value;
   var dw=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value;//技术单位-采购单位
   var caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
   var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
   if(marid!="")
   {
      var shuliang=tr[index].getElementsByTagName("td")[9].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");//数量
      var clcd=tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
      var cldzh=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;

       if(fixsize=="Y")
       {
          //材料总重
          tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
          if(bancai=="型"||bancai=="圆钢"||caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
          {
             //材料总长
             tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=(clcd*shuliang).toFixed(2);
          }
       }
       else
       {
          GetXiShu_TMBluk(fixsize);
          if(bancai=="型"||bancai=="圆钢"||caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
          {
             //材料总重
             tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=(cldzh*shuliang*parseFloat(Xishu_X_Shape)).toFixed(2);

             //材料总长
             tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=(clcd*shuliang*parseFloat(Xishu_X_Shape)).toFixed(2);
          }
          else if(bancai=="板")
          {
             //材料总重
             tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=(cldzh*shuliang*parseFloat(Xishu_B_Shape)).toFixed(2);
          }
          else
          {
             //材料总重
             tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
          }
       }
   }
}


function BomCheckMaoPi()
{
    var table=document.getElementById (getClientId().Id1);
    var tablerows=table.rows.length;
         //存在物料编码的毛坯形状不能为空
     var marid="";
     var mapxingzhuang="";
     var danwei="";
     var cailiaozongchang="";
     for(var i=1;i<tablerows;i++)
     {        
        marid=table.rows[i].cells[3].getElementsByTagName("input")[0].value;
        mapxingzhuang=table.rows[i].cells[27].getElementsByTagName("input")[0].value;
        danwei=table.rows[i].cells[25].getElementsByTagName("input")[0].value;
        cailiaozongchang=table.rows[i].cells[24].getElementsByTagName("input")[0].value;
        if(marid!=""&&mapxingzhuang=="")
        {
           alert("第"+i+"行【毛坯】为空，请输入！！！");
           table.rows[i].cells[27].getElementsByTagName("input")[0].focus();
           table.rows[i].cells[27].getElementsByTagName("input")[0].style.background="yellow";
           return false;
        }
        
        if((danwei.indexOf("(米-")>-1||danwei.indexOf("-米)")>-1)&&cailiaozongchang=="")
        {
           alert("第"+i+"行物料的【材料总长】为空，请输入！！！\r\r提示:该物料采购单位或辅助单位为\"米\"");
           table.rows[i].cells[24].getElementsByTagName("input")[0].focus();
           table.rows[i].cells[24].getElementsByTagName("input")[0].style.background="yellow";
           return false;
        }
     }
     table.rows[i-1].cells[27].getElementsByTagName("input")[0].style.background="white";
     table.rows[i-1].cells[24].getElementsByTagName("input")[0].style.background="white";
     return CheckNumNotZeroWithPurUnit(1);
}

//毛坯形状改变
function ChangeofMarShapeBluck(obj)
{
   var table=document.getElementById (getClientId().Id1);
   var tr=table.getElementsByTagName("tr");
   var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
   var fixsize=tr[index].getElementsByTagName("td")[32].getElementsByTagName("select")[0].value;
   var marid=tr[index].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
   if(marid!="")
   {
       var input=tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0];
       autoLW(input);
   }
}


//方向键，不包含可以隐藏列的
//Table的←↑→↓控制
function grControlFocusWithoutHiddden(input)
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
          if( tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=="hidden")
          {
              continue;
          }
          else
          {
              tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].focus();
              tr[rowIndex].style.backgroundColor ='#55DF55';
              break;
          }
      }
  }
  
  if(key==38)  //向上
  {
        for(var i=rowIndex-1;i>0;i--)
        {
           if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
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
  }
  
  if(key==39)  //向右
  {
        for(var i=cellIndex+1;i<cellcount;i++)
        {
            if(i<cellcount-1)
            {
                 if(tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=='hidden')//
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
             else
             {
               break;
             }
        } 
   } 
  
  if(key==40)   //向下
  {
     for(var i=rowIndex+1;i<rowcount;i++)
     {
       if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
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
  }
}

function grControlFocusWithoutHiddden2(input)
{
  var e=event.srcElement;
  var rowIndex=e.parentNode.parentNode.rowIndex ; //获取行号
  ////////////var cellIndex=e.parentNode.cellIndex;  //获取焦点的列号
  var tr=e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
  var rowcount=tr.length;  //行数
  var td=e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
  var cellcount=td.length;    //列数
  var key=window.event.keyCode;   //获得按钮的编号
  
  var cellIndex=GetRealCellIndex(input.id);
  
  if(key==37)   //向左 
  {
  //是否为第一列
      for(var i=cellIndex-1;i>0;i--)
      {
          if( tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=="hidden")
          {
              continue;
          }
          else
          {
              tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].focus();
              tr[rowIndex].style.backgroundColor ='#55DF55';
              break;
          }
      }
  }
  
  if(key==38)  //向上
  {
        for(var i=rowIndex-1;i>0;i--)
        {
           if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
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
  }
  
  if(key==39)  //向右
  {
        for(var i=cellIndex+1;i<cellcount;i++)
        {
            if(i<cellcount)
            {
                 if(tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=='hidden')//
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
             else
             {
               break;
             }
        } 
   } 
  
  if(key==40)   //向下
  {
     for(var i=rowIndex+1;i<rowcount;i++)
     {
       if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
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
  }
}

function GetRealCellIndex(id)
{
   ///alert(id);
   var retvalue=0;
   if(id.indexOf("_ch_name")>-1)
   {
      retvalue=5;
   }
   else if(id.indexOf("_en_name")>-1)
   {
      retvalue=30;
   }
   else 
   {
       var hmtlid=id.substring(id.lastIndexOf('_'),id.length); 
       switch(hmtlid)
       {
            case "_tuhao":retvalue=2;break;
            case "_marid":retvalue=3;break;
            case "_zongxu":retvalue=4;break;
            case "_ch_name":retvalue=5;break;
            case "_beizhu":retvalue=6;break;
            case "_cailiaocd":retvalue=7;break;
            case "_cailiaokd":retvalue=8;break;
            case "_shuliang":retvalue=9;break;
            case "_dzh":retvalue=10;break;
            case "_cailiaodzh":retvalue=11;break;
            case "_cailiaozongzhong":retvalue=12;break;
            case "_bgzmy":retvalue=13;break;
            case "_tudz":retvalue=14;break;
            case "_tucz":retvalue=15;break;
            case "_tubz":retvalue=16;break;
            case "_tuwt":retvalue=17;break;
            case "_caizhi":retvalue=18;break;
            case "_guige":retvalue=19;break;
            case "_cailiaoname":retvalue=20;break;
            case "_cailiaoguige":retvalue=21;break;
            case "_lilunzhl":retvalue=22;break;
            case "_zongzhong":retvalue=23;break;
            case "_cailiaozongchang":retvalue=24;break;
            case "_labunit":retvalue=25;break;
            case "_biaozhun":retvalue=26;break;
            case "_xinzhuang":retvalue=27;break;
            case "_zhuangtai":retvalue=28;break;
            case "_process":retvalue=29;break;
            case "_en_name":retvalue=30;break;
            case "_ddlKeyComponents":retvalue=31;break;
            case "_ddlFixedSize":retvalue=32;break;
//////////            case "_ddlWmp":retvalue=33;break;
//////////            case "_ku":retvalue=34;break;
            default:break;
       }
   }
   return retvalue;
}



function grControlFocusWithoutHidddenPanit(input)
{
  var e=event.srcElement;
  var rowIndex=e.parentNode.parentNode.rowIndex ; //获取行号
  var cellIndex=e.parentNode.cellIndex;  //获取焦点的列号
  var tr=e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
  var rowcount=tr.length;  //行数
  var td=e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
  var cellcount=td.length;    //列数
  var key=window.event.keyCode;   //获得按钮的编号
  
  //////////var cellIndex=GetRealCellIndex(input.id);
  
  if(key==37)   //向左 
  {
  //是否为第一列
      for(var i=cellIndex-1;i>0;i--)
      {
          if( tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=="hidden")
          {
              continue;
          }
          else
          {
              tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].focus();
              tr[rowIndex].style.backgroundColor ='#55DF55';
              break;
          }
      }
  }
  
  if(key==38)  //向上
  {
        for(var i=rowIndex-1;i>0;i--)
        {
           if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
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
  }
  
  if(key==39)  //向右
  {
        for(var i=cellIndex+1;i<cellcount;i++)
        {
            if(i<cellcount)
            {
                 if(tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=='hidden')//
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
             else
             {
               break;
             }
        } 
   } 
  
  if(key==40)   //向下
  {
     for(var i=rowIndex+1;i<rowcount;i++)
     {
       if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
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
  }
}



function GetXiShu_TMBluk(fixsize)
{
   if(fixsize=="Y")
   {
      Xishu_B_Shape="1";
      Xishu_X_Shape="1";
   }
   else
   {
       Xishu_B_Shape=document.getElementById (getClientId().BXishu).value;
       Xishu_X_Shape=document.getElementById (getClientId().XXishu).value;
   }
}