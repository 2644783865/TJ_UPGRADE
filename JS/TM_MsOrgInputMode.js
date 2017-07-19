var Xishu_B_Shape;
var Xishu_X_Shape;
   
        /*材料计划JS计算：*/
        function AutoTuHao(obj)
        {
           if(document.getElementById("ckbTuhao").checked)
           {
                if(obj.value!="")
                {
                    var table=document.getElementById (getClientId().Id1);
                    var tablerows=table.rows.length;
                    var tr=table.getElementsByTagName("tr");
                    var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
                    var t=parseInt(index)+1;
                    if(t<tablerows)
                    {
                       if(table.rows[t].cells[4].getElementsByTagName("input")[0].value=="")
                       {
                          table.rows[t].cells[4].getElementsByTagName("input")[0].value=obj.value;
                       }
                    }
                }
            }
        }
        
        //长宽改变
        function auto(input)
        {
            var table=document.getElementById (getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var cailiaocd=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
            var cailiaokd=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度
            var shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");//数量
            var p_shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");//数量
            var dw=tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value;//技术单位-采购单位
            var caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
            var mapishape=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value;
            var fix=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
            GetXiShu_TMOrg(fix);
            var pattem=/^\d+(\.\d+)?$/;//数量验证
            var pattem2=/^[0-9]*$/;//长、宽验证
            
            if(cailiaocd=="")
            {
               cailiaocd="0";
               tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="";
            }
            
            if(cailiaokd=="")
            {
               cailiaokd="0";
               tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="";
            }
            
            if(shuliang=="")
            {
               shuliang="1";
               tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="";
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
                tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="";
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
                tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="";
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
                tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="1";
                return false;
            }
            //格式检查完毕，开始处理数据
            var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaoname=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var dzh=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
            
            //////////alert(marid);
            
            if(marid!="")
            {
               var cailiaodzh="0";
               var cailiaozongzhong="0";
               var bgzmy="0";
               var guige=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
               var cailiaoguige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var lilunzhl=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var zongzhong="0";
               var cailiaozongchang=tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var sub_marid=marid.substring(0, 5);
               var child_sub_marid = sub_marid.substring(0, 2);
               if(sub_marid != "01.01") //标准件
               {
                        if (mapishape=="板"||cailiaoname.indexOf("钢板")>-1||cailiaoname.indexOf("栅格板")>-1 || cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("钢板网")>-1) 
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
                                dzh=cailiaodzh;//实际单重不根据材料单重计算
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
                            cailiaozongchang=cailiaocd*shuliang;  
                            dzh=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*cailiaocd/1000;//实际单重
                            cailiaodzh=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*cailiaocd/1000;//材料单重
                            cailiaozongzhong=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*p_shuliang*cailiaocd/1000;
                            zongzhong=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*shuliang*cailiaocd/1000;
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
                        
                        tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=dzh;
                        tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=cailiaodzh;
                        tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=cailiaozongzhong;
                        tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=bgzmy;
                        tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=guige;
                        tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=zongzhong;
                        tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=cailiaozongchang;
                        if(tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=="")//图纸上单重
                        {
                           tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value="0";
                        }   
                    }
                    else if(marid.indexOf("01.01")>-1)
                    {
                        if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                        {
                            tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=cailiaocd*p_shuliang;  
                            tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*cailiaocd/1000;//实际单重
                            tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*cailiaocd/1000;//材料单重
                            tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*p_shuliang*cailiaocd/1000;
                            tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*shuliang*cailiaocd/1000;
                        }
                        else
                        {
                            tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;//实际单重
                            tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;//材料单重
                            tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*shuliang;
                            tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*shuliang;
                        }
                    }
                    //采购单位为平米时计算面域
                    if(caigoudw.indexOf("(平米")>-1||caigoudw.indexOf("(平方米")>-1||caigoudw.indexOf("(m2")>-1||caigoudw.indexOf("(M2")>-1||caigoudw.indexOf("(㎡")>-1)
                    {
                        tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[1].value=(cailiaocd*cailiaokd/1000000).toFixed(2);
                    }
                    //型材面域计算
                    AutoMarAreaByLenShape(cailiaocd,mapishape,marid,tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0]);
            }
            else
            {
               if(input.parentNode.cellIndex!=9)//物料编码为空时，输入长宽无效
               {
                  if(parseInt(cailiaocd)>0||parseInt(cailiaokd)>0)
                  {
                    tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="0";
                    tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="0";
                    alert("提示:物料编码为空，输入【材料长度】【材料宽度】无效！！！");
                  }
               }
               else//如果输入数量，要修改实际总重
               {
                   if(dzh!="")
                   {
                       tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=dzh*shuliang;
                   }
               }
            }
        }
        
        //数量改变
        function autoshuliang(input)
        {
            var table=document.getElementById (getClientId().Id1);
            
            var number=1;
            if(document.getElementById(getClientId().Id4)!=null)
            {
               number=document.getElementById(getClientId().Id4).value;
            }
            
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var cailiaocd=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
            var cailiaokd=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度
            var shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//数量
            shuliang=shuliang*number;
            tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[1].value=shuliang;
            tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[2].value=shuliang;
            var mapishape=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value;
            var dw=tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value;//技术单位-采购单位
            var caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
            var fix=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
            GetXiShu_TMOrg(fix);

            var pattem=/^\d+(\.\d+)?$/;//数量验证
            var pattem2=/^[0-9]*$/;//长、宽验证
            
            if(cailiaocd=="")
            {
               cailiaocd="0";
               tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="";
            }
            
            if(cailiaokd=="")
            {
               cailiaokd="0";
//               tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="";
            }
            
            if(shuliang=="")
            {
               shuliang="1";
               tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="";
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
                tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="";
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
                tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="";
                return false;
            }
            //数量检查
            if(pattem.test(shuliang))
            {
               if(parseInt(shuliang)>50)
               {
                  if(input.parentNode.cellIndex==9)
                  {
                     alert('提示:输入【单台数量】超出50,请核实！！!');
                  }  
               }
            }
            else
            {
                alert('提示:输入【单台数量】格式不正确!');
                tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="";
                return false;
            }
            //格式检查完毕，开始处理数据
            var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaoname=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var dzh=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
            if(marid!="")
            {
               var cailiaodzh=tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
               var cailiaozongzhong=tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var bgzmy=tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var guige=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
               var cailiaoguige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var lilunzhl=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var zongzhong=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var cailiaozongchang=tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
               var sub_marid=marid.substring(0, 5);
               var child_sub_marid = sub_marid.substring(0, 2);
               if(child_sub_marid != "02"||child_sub_marid=="02") //去掉非低值易耗品限制
               {
                      if(marid.indexOf("01.01")>-1)//理论重量*数量
                       {
                            if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                            {
                                cailiaozongchang=(cailiaocd*shuliang).toFixed(2);  
                                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=(lilunzhl*cailiaocd/1000).toFixed(2);
                                tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=(lilunzhl*cailiaocd/1000).toFixed(2);
                                cailiaozongzhong=(lilunzhl*shuliang*cailiaocd/1000).toFixed(2);
                                zongzhong=(lilunzhl*shuliang*cailiaocd/1000).toFixed(2);
                            }
                            else
                            {
                                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;
                                tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;
                                cailiaozongzhong=(tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                                zongzhong=(tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                            }    
                       }
                       else if (mapishape=="板"||cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢板网")>-1||cailiaoname.indexOf("钢格板")>-1) 
                       {
                            ////A
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
                                cailiaodzh = (cailiaocd * lilunzhl / 1000).toFixed(2);
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
                            cailiaozongchang=(cailiaocd*shuliang).toFixed(2);  
                            dzh=(lilunzhl*cailiaocd/1000).toFixed(2);
                            cailiaodzh=(lilunzhl*cailiaocd/1000).toFixed(2);
                            cailiaozongzhong=(lilunzhl*shuliang*cailiaocd/1000).toFixed(2);
                            zongzhong=(lilunzhl*shuliang*cailiaocd/1000).toFixed(2);
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
                        tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=cailiaozongzhong;
                        tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=zongzhong;
                        tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=cailiaozongchang;
                        if(tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=="")//图纸上单重
                        {
                           tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value="0";
                        }   
                    }
            }
            else
            {
               if(input.parentNode.cellIndex!=9)//物料编码为空时，输入长宽无效
               {
                  if(parseInt(cailiaocd)>0||parseInt(cailiaokd)>0)
                  {
                    tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="0";
                    tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="0";
                    alert("提示:物料编码为空，输入【材料长度】【材料宽度】无效！！！");
                  }
               }
               else//如果输入数量，要修改实际总重
               {
                   if(dzh!="")
                   {
                       tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=dzh*shuliang;
                   }
               }
            }
        }
        
        //计划数量改变
        function autop_shuliang(input)
        {
            var table=document.getElementById (getClientId().Id1);
                        
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var cailiaocd=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
            var shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");//总数量
            var p_shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");//计划数量
            var mapishape=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value;
            var dw=tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value;//技术单位-采购单位
            var caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
            var fix=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
            GetXiShu_TMOrg(fix);

            var pattem=/^\d+(\.\d+)?$/;//数量验证
            
            if(shuliang=="")
            {
                alert("请先输入单台数量！！！");
                input.value="";
                return false;
            }
            //数量检查
            if(pattem.test(p_shuliang))
            {
               if(parseInt(p_shuliang)<parseInt(shuliang))
               {
                  alert("【计划数量】不能小于【总数量】！！！");
                  input.value=shuliang;
                  autop_shuliang(input);
                  return false;
               }
               else
               {
                  var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                  var cailiaocd=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
                  var cailiaokd=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度
                  var cailiaoname=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                  var dzh=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
                  if(marid!="")
                  {//marid
                           var cailiaodzh=tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
                           var dzh=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
                           var cailiaozongzhong=tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           var bgzmy=tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           var guige=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
                           var cailiaoguige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           var lilunzhl=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           var zongzhong=tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           var cailiaozongchang=tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           var sub_marid=marid.substring(0, 5);
                           var child_sub_marid = sub_marid.substring(0, 2);
                           if(child_sub_marid != "02"||child_sub_marid=="02") //非低值易耗品02和标准件
                           {
                                  if(marid.indexOf("01.01")>-1)//理论重量*数量
                                   {
                                        if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
                                        {
                                            tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=(cailiaocd*p_shuliang).toFixed(2);  
                                            tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=(lilunzhl*cailiaocd/1000).toFixed(2);
                                            tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=(lilunzhl*cailiaocd/1000).toFixed(2);
                                            cailiaozongzhong=(lilunzhl*p_shuliang*cailiaocd/1000).toFixed(2);
                                            zongzhong=(lilunzhl*shuliang*cailiaocd/1000).toFixed(2);
                                        }
                                        else
                                        {
                                            tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;
                                            tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value;
                                            cailiaozongzhong=(tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*p_shuliang).toFixed(2);
                                            zongzhong=(tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value*shuliang).toFixed(2);
                                        }    
                                   }
                                   else if (mapishape=="板"||cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢板网")>-1||cailiaoname.indexOf("钢格板")>-1) 
                                   {
                                        ////A
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
                                        cailiaozongchang=(cailiaocd*p_shuliang).toFixed(2);  
                                        dzh=(lilunzhl*cailiaocd/1000).toFixed(2);
                                        cailiaodzh=(lilunzhl*cailiaocd/1000).toFixed(2);
                                        cailiaozongzhong=(lilunzhl*p_shuliang*cailiaocd/1000).toFixed(2);
                                        zongzhong=(lilunzhl*shuliang*cailiaocd/1000).toFixed(2);
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
                                    tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=cailiaozongzhong;
                                    tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=zongzhong;
                                    tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=cailiaozongchang;
                                    if(tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=="")//图纸上单重
                                    {
                                       tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value="0";
                                    }   
                                }
                  }//--marid
                  else
                  {
                     alert("物料编码为空，不能修改【计划数量】！！！");
                     input.value=shuliang;
                     return false;
                  }
               }
            }
            else
            {
                alert("请输入正确的数值格式！！！");
                input.value=shuliang;
                autop_shuliang(input);
                input.select();
                return false;
            }
        }
        
        //实际单重改变
        function auto1(input)
        {
            var table=document.getElementById (getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var dzh=document.getElementById(input.id).value;
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[1].value;
            var cailiaoname=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaodz=tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;//材料单重
            var shape=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//毛坯形状
            var llzl=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//理论重量
            var guige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); //材料规格
            var fix=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
            GetXiShu_TMOrg(fix);
            
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
               input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="";
            }
            
            if(pattem.test(dzh)&&pattem.test(shuliang))
            {
               //计算总重
               input.parentNode.parentNode.getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=(dzh*shuliang).toFixed(2);
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
               if((shape=="板"&&pattem.test(guige))||cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1)
               {
                   var bgzmy=0;
                   if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1)
                   {
                       bgzmy=(dzh/llzl).toFixed(2);
                   }
                   else
                   {
                       bgzmy=(dzh/(llzl*guige)).toFixed(2);
                   }
                   tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=bgzmy;
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
        function auto2(input)
        {
            var table=document.getElementById (getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cldzh=document.getElementById(input.id).value;
            var lilunzhl=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var shuliang=input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[1].value.replace(/(\s*$)/g, "");
            
            var p_shuliang=input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");
            
            var dzh=input.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
            var bgmy=input.parentNode.parentNode.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var cailiaoguige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var blankshape=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var fix=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
            GetXiShu_TMOrg(fix);

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
               shuliang="1";
               input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="1";
            }
            
            if(pattem.test(cldzh)&&pattem.test(shuliang))
            {
                if(cldzh!=0)
                {
                    //材料总重
                    if(blankshape.indexOf("板")>-1)
                    {
                        input.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cldzh*p_shuliang*parseFloat(Xishu_B_Shape)).toFixed(2);
                    }
                    else if(blankshape.indexOf("型")>-1||blankshape=="圆钢")
                    {
                        input.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cldzh*p_shuliang).toFixed(2);
                        //材料长度
                        input.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=(cldzh*1000/lilunzhl).toFixed(0);
                        //材料总长
                        input.parentNode.parentNode.getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=(cldzh*p_shuliang*parseFloat(Xishu_X_Shape)*1000/lilunzhl).toFixed(0);
                    }
                    else
                    {
                        input.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cldzh*p_shuliang).toFixed(2);
                    }
                    
                    if(dzh==""||dzh==0)//如果实际单重为空或0，重新计算
                    {
                       //实际单重
                       input.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=cldzh;
                       //总重
                       input.parentNode.parentNode.getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
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
        function automy(input)
        {
            var shuliang=input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[1].value;
            var my=document.getElementById(input.id).value;
            var table=document.getElementById (getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");

            var cailiaocd=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//长度
            var cailiaokd=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//宽度

            var cailiaoguige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var lilunzhl=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");

            var miany=(cailiaocd*cailiaokd/1000000).toFixed(2);
            
            var marshape=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//毛坯形状
                        
            var txt=tr[index].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;//条件属性
            ///alert(txt);

            var fix=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
            GetXiShu_TMOrg(fix);

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
               shuliang="1";
               input.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="";
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
                var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
                var cailiaoname=tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value;
                if(marid!="")//start marid
                {
                               var dzh=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");//实际单重
                               var cailiaodzh="0";
                               var cailiaozongzhong="0";
                               var bgzmy=my;//单个面域
                               var guige=tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, ""); 
                               var cailiaoguige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                               var lilunzhl=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                               var zongzhong="0";
                               var cailiaozongchang=tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                               var sub_marid=marid.substring(0, 5);
                               var child_sub_marid = sub_marid.substring(0, 2);
                               if(sub_marid != "01.01") //STR标准件
                               {
                                   //START A
                                   if (marshape=="板"||cailiaoname.indexOf("钢板")>-1 || cailiaoname.indexOf("钢格板")>-1|| cailiaoname.indexOf("栅格板")>-1|| cailiaoname.indexOf("花纹板")>-1 )//
                                   {
                                     //START B 
                                     if(bgzmy==0)
                                     {
                                        if (cailiaocd != 0 && cailiaokd != 0)
                                        {
                                            guige = cailiaoguige + 'x' + cailiaokd + '+' + cailiaocd;
                                            if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("钢板网")>-1||cailiaoname.indexOf("栅格板")>-1)
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
                                            cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                            
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
                                            cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                        }
                                     }
                                     else //面域不为零时，根据面域计算重量
                                     {
                                            if(txt.indexOf("j")>-1||txt.indexOf("J")>-1||txt.indexOf("y")>-1||txt.indexOf("Y")>-1||txt=="协A"||txt=="协B"||txt=="成交"||txt=="半"||txt=="半退"||txt=="半正"||txt=="半调")
                                            {
                                                  if(txt.indexOf("j")>-1||txt.indexOf("J")>-1)
                                                  {
                                                     var num=txt.substring(1,txt.length);
                                                     var pattem=/^[1-9][0-9]*$/;
                                                     if(pattem.test(num))
                                                     { 
                                                        if(marshape=="板")
                                                        {
                                                           var gg=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                                                           if(pattem.test(gg)&&parseInt(gg)>=parseInt(num))
                                                           {
                                                               cailiaoguige=parseInt(gg)-parseInt(num);
                                                               alert(cailiaoguige);
                                                           }
                                                           else
                                                           {
                                                              alert("材料规格不正确或小于输入条件属性数值！！！");
                                                           }
                                                        }
                                                        else
                                                        {
                                                            alert("非板材，输入条件属性\""+txt+"\"无效！！！");
                                                        }
                                                    }
                                                 }
                                            }
                                            
                                            guige = cailiaoguige + 'x' + cailiaokd + '+' + cailiaocd;
                                            if(cailiaodzh=0||cailiaodzh=="")
                                            {
                                              if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("栅格板")>-1||cailiaoname.indexOf("钢格板")>-1)
                                                {
                                                   cailiaodzh = (bgzmy * lilunzhl).toFixed(2);
                                                }
                                                else
                                                {
                                                   cailiaodzh = (cailiaoguige * bgzmy * lilunzhl).toFixed(2);
                                                }
                                               cailiaozongzhong = (cailiaodzh * shuliang * parseFloat(Xishu_B_Shape)).toFixed(2);
                                            }
                                            else
                                            {
                                                cailiaodzh = tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                                                cailiaozongzhong = tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                                            }
                                            
                                            if(cailiaoname.indexOf("花纹板")>-1||cailiaoname.indexOf("钢格板")>-1||cailiaoname.indexOf("栅格板")>-1)
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
                                //END A
                                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=dzh;
                                tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=cailiaodzh;
                                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=cailiaozongzhong;
                                tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=bgzmy;
                                tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=guige;
                                tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=zongzhong;
                                tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=cailiaozongchang;
                                
                                if(tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=="")//图纸上单重
                                {
                                   tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value="0";
                                }   
                            }
                    }
                    else//物料编码为空时，输入面域无效
                    {
                       if(input.parentNode.cellIndex!=13)
                       {
                          if(parseInt(my)>0)
                          {
                            tr[index].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value="0";
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
        function TudanZhong(input)
        {
            var tudz=document.getElementById(input.id).value;
            var pattem=/^\d+(\.\d+)?$/;
            if(!pattem.test(tudz))
            {
               document.getElementById(input.id).value="";
               alert("提示:输入【图纸上单重】格式不正确！！！");
            }

        }
        
        //图纸上单重计算
        function TudanZhongBom(input)
        {
            var table=document.getElementById (getClientId().Id1);
            
            
            var tr=table.getElementsByTagName("tr");
            var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            
            var tudz=document.getElementById(input.id).value;
            var real=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;
            var pattem=/^\d+(\.\d+)?$/;
            if(!pattem.test(tudz))
            {
               document.getElementById(input.id).value="";
               alert("提示:输入【图纸上单重】格式不正确！！！");
               return;
            }
            
            
            if(document.getElementById("ctl00_PrimaryContent_ckbUnitWght").checked)
            {
                var number=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[1].value;
                if(number=="")
                {
                   number=1;
                }
                //实际单重=图纸上单重
                real=input.value;
                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=real;
                //总重
                tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=parseFloat(real)*number;
            }
            CheckUnitWght_TuUnit(input);
        }
        
        function CheckUnitWght_TuUnit(input)
        {
           var table=document.getElementById (getClientId().Id1);
           var tr=table.getElementsByTagName("tr");
           var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
        
           var obj_real=tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0];
           var obj_tu=tr[index].getElementsByTagName("td")[18].getElementsByTagName("input")[0];
           
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
        
        function CheckOrgTJSX(obj)
        {
            var txt=obj.value.replace(/(\s*$)/g, "");
            var table=document.getElementById (getClientId().Id1);
            var tr=table.getElementsByTagName("tr");
            var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var mpxz=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var mar=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var len=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var width=tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            var mpnumber=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");
            var lilunwght=tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
            
            if(mar=="")
            {
                alert("【物料编码】为空,输入条件属性无效！！！");
                return false;
            }
            
           if(txt!="")
           {
               if(txt.indexOf("j")>-1||txt.indexOf("J")>-1||txt.indexOf("y")>-1||txt.indexOf("Y")>-1||txt=="协A"||txt=="协B"||txt=="成交"||txt=="半"||txt=="半退"||txt=="半正"||txt=="半调")
               {
                  if(txt.indexOf("j")>-1||txt.indexOf("J")>-1)
                  {
                     var num=txt.substring(1,txt.length);
                     var pattem=/^[1-9][0-9]*$/;
                     if(pattem.test(num))
                     { 
                        if(mpxz=="板")
                        {
                           var guige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           if(pattem.test(guige)&&parseInt(guige)>=parseInt(num))
                           {
                               var real=parseInt(guige)-parseInt(num);
                               tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=real+"x"+width+"+"+len;
                               //实际单重
                               tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=(len*width*real*lilunwght/1000000).toFixed(2);
                               //实际总重
                               tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=(len*width*real*lilunwght*mpnumber/1000000).toFixed(2);
                           }
                           else
                           {
                              alert("材料规格不正确或小于输入条件属性数值！！！");
                              obj.value="";
                           }
                        }
                        else
                        {
                            alert("非板材，输入条件属性\""+txt+"\"无效！！！");
                            obj.value="";
                        }
                     }
                     else
                     {
                         var aa=confirm("无法识别的条件属性列，确认添加吗?");
                         if(aa==false)
                         {
                            obj.value="";
                         }
                     }
                  }   
                  else if(txt.indexOf("Y")>-1||txt.indexOf("y")>-1)
                  {
                     var num=txt.substring(1,txt.length);
                     var pattem=/^[1-9][0-9]*$/;
                     if(pattem.test(num))
                     { 
                        if(mpxz=="圆钢")
                        {
                           var guige=tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
                           var zhijing=guige.substring(1,guige.length);
                           ////alert(zhijing);
                           if(pattem.test(zhijing)&&parseInt(zhijing)>=parseInt(num))
                           {
                               var real=parseInt(zhijing)-parseInt(num);
                               tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value="φ"+real+"+"+len;
                               //找出φreal对应的理论重量
                               var lilun=lilunwght*real*real/(zhijing*zhijing);
                               /////alert(lilun);
                               //实际单重
                               tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=(len*lilun/1000).toFixed(2);
                               //实际总重
                               tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=(len*lilun*mpnumber/1000).toFixed(2);
                           }
                           else
                           {
                              alert("材料规格不正确或小于输入条件属性数值！！！");
                              obj.value="";
                           }
                        }
                        else
                        {
                            alert("非圆钢，输入条件属性\""+txt+"\"无效！！！");
                            obj.value="";
                        }
                     }
                     else
                     {
                         var aa=confirm("无法识别的条件属性列，确认添加吗?");
                         if(aa==false)
                         {
                            obj.value="";
                         }
                     }                     
                  }
                  else
                  {
                      tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value="采";
                      tr[index].getElementsByTagName("td")[32].getElementsByTagName("input")[0].value=obj.value;
                  }
               }
               else
               {
                     var aa=confirm("无法识别的条件属性列，确认添加吗?");
                     if(aa==false)
                     {
                        obj.value="";
                     }
               }
           }
        }        
//材料总重改变        
function MarTotalWeightChange(obj)
{
   var checktxt=obj.value;
   var pattem=/^\d+(\.\d+)?$/;
   var table=document.getElementById (getClientId().Id1);
   var tr=table.getElementsByTagName("tr");
   var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
   var cailiaodz=tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
   var shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
   if(!pattem.test(checktxt))
   {
      alert("请输入正确的数值格式！！！");
      obj.value="";
   }
}

//控制定尺是否可用
function CtrlFixSize(obj)
{
   var table=document.getElementById (getClientId().Id1);
   var tr=table.getElementsByTagName("tr");
   var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
   var bancai=tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value;
   var fixsize=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
   var dw=tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value;//技术单位-采购单位
   var caigoudw=dw.substring(dw.indexOf('-')+1,dw.length);
   var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
   if(marid!="")
   {
      var shuliang=tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[2].value.replace(/(\s*$)/g, "");//数量
      var clcd=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
      var cldzh=tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;

       if(fixsize=="Y")
       {
          //材料总重
          tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
          if(bancai=="型"||bancai=="圆钢"||caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
          {
             //材料总长
             tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=(clcd*shuliang).toFixed(2);
          }
       }
       else
       {
          GetXiShu_TMOrg(fixsize);
          if(bancai=="型"||bancai=="圆钢"||caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
          {
             //材料总重
             tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cldzh*shuliang*parseFloat(Xishu_X_Shape)).toFixed(2);

             //材料总长
             tr[index].getElementsByTagName("td")[28].getElementsByTagName("input")[0].value=(clcd*shuliang*parseFloat(Xishu_X_Shape)).toFixed(2);
          }
          else if(bancai=="板")
          {
             //材料总重
             tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cldzh*shuliang*parseFloat(Xishu_B_Shape)).toFixed(2);
          }
          else
          {
             //材料总重
             tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cldzh*shuliang).toFixed(2);
          }
       }
   }
}

//毛坯形状改变
function ChangeofMarShape(obj)
{
   var table=document.getElementById (getClientId().Id1);
   var tr=table.getElementsByTagName("tr");
   var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
   var fixsize=tr[index].getElementsByTagName("td")[36].getElementsByTagName("select")[0].value;
   var marid=tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value.replace(/(\s*$)/g, "");
   if(marid!="")
   {
       var input=tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0];
       auto(input);
   }
}



/***************************************************
↑↓←→键控制，增加了对隐藏列及无法获取焦点列的控制
****************************************************/
//Table的←↑→↓控制
function grControlFocus(input)
{
  var e=event.srcElement;
  var rowIndex=e.parentNode.parentNode.rowIndex ; //获取行号
  //var cellIndex=e.parentNode.cellIndex;  //获取焦点的列号
  var tr=e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
  var rowcount=tr.length;  //行数
  var td=e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
  var cellcount=td.length;    //列数
  /////  alert('共'+cellcount+'列;当前列'+cellIndex);
 ///// alert(td);
 /////var hmtlid=input.id.substring(input.id.lastIndexOf('_'),input.id.length); 
 /////alert(hmtlid);
 var cellIndex=GetRealCellIndex(input.id);
 /////alert(cellIndex);
  var key=window.event.keyCode;   //获得按钮的编号
 
 
  if(key==37)   //向左 
  {
      //是否为第一列
      for(var i=cellIndex-1;i>0;i--)
      {
          //alert(i);
          if( tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=="hidden")
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
                                    tr[rowIndex].style.backgroundColor ='#55DF55';

                   tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].focus();
                   tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].select();
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
      retvalue=7;
   }
   else if(id.indexOf("_en_name")>-1)
   {
      retvalue=34;
   }
   else 
   {
       var hmtlid=id.substring(id.lastIndexOf('_'),id.length); 
       switch(hmtlid)
       {
            case "_txtMSXuhao":retvalue=2;break;
            case "_txtXuhao":retvalue=3;break;
            case "_tuhao":retvalue=4;break;
            case "_marid":retvalue=5;break;
            case "_marid":retvalue=5;break;
            case "_zongxu":retvalue=6;break;
            case "_ch_name":retvalue=7;break;
            case "_ddlIsManu":retvalue=8;break;
            case "_beizhu":retvalue=9;break;
            case "_cailiaocd":retvalue=10;break;
            case "_cailiaokd":retvalue=11;break;
            case "_shuliang":retvalue=12;break;
            case "_dzh":retvalue=13;break;
            case "_cailiaodzh":retvalue=14;break;
            case "_cailiaozongzhong":retvalue=15;break;
            case "_bgzmy":retvalue=16;break;
            case "_tjsx":retvalue=17;break;
            case "_tudz":retvalue=18;break;
            case "_tucz":retvalue=19;break;
            case "_tubz":retvalue=20;break;
            case "_tuwt":retvalue=21;break;
            case "_caizhi":retvalue=22;break;
            case "_guige":retvalue=23;break;
            case "_cailiaoname":retvalue=24;break;
            case "_cailiaoguige":retvalue=25;break;
            case "_lilunzhl":retvalue=26;break;
            case "_zongzhong":retvalue=27;break;
            case "_cailiaozongchang":retvalue=28;break;
            case "_labunit":retvalue=29;break;
            case "_biaozhun":retvalue=30;break;
            case "_xinzhuang":retvalue=31;break;
            case "_zhuangtai":retvalue=32;break;
            case "_process":retvalue=33;break;
            case "_en_name":retvalue=34;break;
            case "_ddlKeyComponents":retvalue=35;break;
            case "_ddlFixedSize":retvalue=36;break;
            case "_ddlWmp":retvalue=37;break;
            case "_ku":retvalue=38;break;
            default:break;
       }
   }
   return retvalue;
}


function getSelect(obj)
{
  var objtr=obj.parentNode.parentNode;
  objtr.style.backgroundColor ='#55DF55';

}

function Fast_Op(obj)
{
   var control_id=obj.id;
   var tuhao=document.getElementById("ctl00_PrimaryContent_ckbTuhao");
   var jztuhao=document.getElementById("ctl00_PrimaryContent_ckbJZTuhao");
   var xuhao=document.getElementById("ctl00_PrimaryContent_ckbXuhao");
   var jzxuhao=document.getElementById("ctl00_PrimaryContent_ckbJZXuhao");
   var flag;
   if(control_id=="ctl00_PrimaryContent_ckbTuhao"||control_id=="ctl00_PrimaryContent_ckbJZTuhao")
   {
      flag="0";
   }
   else if(control_id=="ctl00_PrimaryContent_ckbXuhao"||control_id=="ctl00_PrimaryContent_ckbJZXuhao")
   {
      flag="1";
   }
   
   switch(flag)
   {
       case "0":
               if(obj.checked)
               {
                  tuhao.checked=false;
                  jztuhao.checked=false;
                  obj.checked=true;
               }
               break;
       case "1":
               if(obj.checked)
               {
                  xuhao.checked=false;
                  jzxuhao.checked=false;
                  obj.checked=true;
               }
               break;
       default:
               break;
   }
   
   var note="当前:";
   if(tuhao.checked)
   {
      note+="图";
   }
   if(jztuhao.checked)
   {
      note+="前图";
   }
   if(xuhao.checked)
   {
      if(note=="当前:")
      {
         note+="序";
      }
      else
      {
        note+="+序";
      }
   }
   if(jzxuhao.checked)
   {
      if(note=="当前:")
      {
         note+="前序";
      }
      else
      {
        note+="+前序";
      }
   }
   if(note=="当前:")
   {
      note ="("+note +"无)";
   }
   else
   {
      note ="("+note +")";
   }
   
   document.getElementById("ctl00_PrimaryContent_lblshortcut").value=note;
}

//BOM录入界面图号的两种操作方式
function OrgAutoTuHao(obj)
{
   if(document.getElementById("ctl00_PrimaryContent_ckbTuhao").checked)
   {
        if(obj.value!="")
        {
            var table=document.getElementById (getClientId().Id1);
            var tablerows=table.rows.length;
            var tr=table.getElementsByTagName("tr");
            var index=obj.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            var t=parseInt(index)+1;
            if(t<tablerows)
            {
               if(table.rows[t].cells[4].getElementsByTagName("input")[0].value=="")
               {
                  table.rows[t].cells[4].getElementsByTagName("input")[0].value=obj.value;
               }
            }
        }
    }
    else if(document.getElementById("ctl00_PrimaryContent_ckbJZTuhao").checked)
    {
       var jz=document.getElementById("ctl00_PrimaryContent_txtJZTuhao").value;
       if(jz!=""&&obj.value!="")
       {
          obj.value=jz+"-"+obj.value;
       }
    }
}

function OrgAutoXuhao(obj)
{
   if(document.getElementById("ctl00_PrimaryContent_ckbJZXuhao").checked)
   {
       var jz=document.getElementById("ctl00_PrimaryContent_txtJZXuhao").value;
       if(jz!=""&&obj.value!="")
       {
          obj.value=jz+"."+obj.value;
       }
       verify(obj);
   }
   var rowindex=obj.parentNode.parentNode.rowIndex;
   obj.parentNode.parentNode.parentNode.rows[rowindex].cells[3].getElementsByTagName("input")[0].value=obj.value;
}
//BOM录入界面数据的重复性提提示
function BomInputCheck()
{
    var table=document.getElementById (getClientId().Id1);
    var tablerows=table.rows.length;
    var array_zongxu=new Array(new Array());
    var array_index=0;
    
    
    for(var i=1;i<tablerows;i++)
    {
        var zx=table.rows[i].cells[6].getElementsByTagName("input")[0].value;
        var name=table.rows[i].cells[7].getElementsByTagName("input")[0].value;
        var xh=table.rows[i].cells[3].getElementsByTagName("input")[0].value;
        if(zx!=""||xh!=""||name!="")
        {
           if(zx=="")
           {
              alert("第【"+i+"】行请输入【总序】！！！");
              return false;
           }
           if(xh=="")
           {
              alert("第【"+i+"】行请输入【序号】！！！");
              return false;
           }
           if(name=="")
           {
              alert("第【"+i+"】行请输入【中文名称】！！！");
              return false;
           }
        }
    }

    for(var i=1;i<tablerows;i++)
    {
        var zx=table.rows[i].cells[6].getElementsByTagName("input")[0].value;
        var marid=table.rows[i].cells[5].getElementsByTagName("input")[0].value;
        var name=table.rows[i].cells[7].getElementsByTagName("input")[0].value;
        if(zx!="")
        {
           array_zongxu[array_index]=new Array();
           array_zongxu[array_index][0]=zx;
           array_zongxu[array_index][1]=marid;
           array_zongxu[array_index][2]=name;
           array_index++;
        }
    }
    if(array_index>1)
    {
        for(var m=0;m<array_index-1;m++)
        {
           for(var n=m+1;n<array_index;n++)
           {
              if(array_zongxu[m][0]==array_zongxu[n][0])
              {
                  if(array_zongxu[m][1]!=array_zongxu[n][1])
                  {
                     alert("提示:无法保存！！！\r\r页面上相同总序【"+array_zongxu[m][0]+"】的物料编码不同！！！");
                     return false;
                  }
                  
                  var zongxu_same=true;
                  if(array_zongxu[m][0]==array_zongxu[n][0])
                  {
                      zongxu_same=confirm("总序【"+array_zongxu[m][0]+"】有多条记录！！！\r\r确认继续吗？");
                      if(zongxu_same)
                      {
                          if((array_zongxu[m][1]==array_zongxu[n][1])&&(array_zongxu[m][2]!=array_zongxu[n][2]))
                          {
                              var yes=confirm("页面上总序【"+array_zongxu[m][0]+"】有多条，名称不同！！！\r\r确认继续保存吗？？？");
                              if(yes==false)
                              {
                                 return false;
                              }
                          }
                      }
                      else
                      {
                          return false;
                      }
                  }
              }
           }
        }
     }
     //存在物料编码的毛坯形状不能为空
     var marid="";
     var mapxingzhuang="";
     var danwei="";
     var cailiaozongchang="";
     for(var i=1;i<tablerows;i++)
     {
        marid=table.rows[i].cells[5].getElementsByTagName("input")[0].value;
        mapxingzhuang=table.rows[i].cells[31].getElementsByTagName("input")[0].value;
        danwei=table.rows[i].cells[29].getElementsByTagName("input")[0].value;
        cailiaozongchang=table.rows[i].cells[28].getElementsByTagName("input")[0].value;
        if(marid!=""&&mapxingzhuang=="")
        {
           alert("第"+i+"行【毛坯】为空，请输入！！！");
           table.rows[i].cells[31].getElementsByTagName("input")[0].focus();
           table.rows[i].cells[31].getElementsByTagName("input")[0].style.background="yellow";
           return false;
        }
        
        if((danwei.indexOf("(米-")>-1||danwei.indexOf("-米)")>-1)&&(cailiaozongchang==""||cailiaozongchang=="0"))
        {
           alert("第"+i+"行物料的【材料总长】为空，请输入！！！\r\r提示:该物料采购单位或辅助单位为\"米\"");
           table.rows[i].cells[28].getElementsByTagName("input")[0].focus();
           table.rows[i].cells[28].getElementsByTagName("input")[0].style.background="yellow";
           return false;
        }
        table.rows[i].cells[31].getElementsByTagName("input")[0].style.background="white";
        table.rows[i].cells[28].getElementsByTagName("input")[0].style.background="white";
     }
     return CheckNumNotZeroWithPurUnit(4);
 }


//验证序号格式
function CheckXuHao(obj)
{
   var xuhao=obj.value;
   if(xuhao!="")
   {
      var pattem=/^1\.0\.([1-9]{1}([0-9]){0,}){1}$|1((\.[1-9]{1}[0-9]{0,}){1,})$/;
      if(!pattem.test(xuhao))
      {
         alert("请输入正确的【序号】格式！！！");
         obj.value="";
      }
   }
}

function GetXiShu_TMOrg(fixsize)
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


/*助记码操作*/
function autoMsOrgCode(input)
{
    var marid=document.getElementById(input.id).value;
    var table=document.getElementById (getClientId().Id1);
    var tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    //所有单元格清空(图号，总序，中文名称，备注，关键部件，定尺除外)
    tr[index].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="0";//长度
    tr[index].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="0";//宽度
    tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="1";//数量默认为1
    tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[1].value=document.getElementById (getClientId().Id4).value;
    tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[2].value=document.getElementById (getClientId().Id4).value;
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
    tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[32].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[33].getElementsByTagName("input")[0].value="";
    tr[index].getElementsByTagName("td")[34].getElementsByTagName("input")[0].value="";    
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
        tr[index].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=sub_marid;
        connSql(1);//连接数据库获取物料信息  MNAME,GUIGE,CAIZHI,TECHUNIT,MWEIGHT,GB
        if(!objrs.BOF&!objrs.EOF)
        {
           var cailiaoname=objrs.Fields(0).Value;
           if(child_marid == "01.01")//标准件
           {
            //MNAME,GUIGE,CAIZHI,TECHUNIT,MWEIGHT,GB
            tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//材料规格
            tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//规格
            tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value=objrs.Fields(2).Value;//材质
            tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value=objrs.Fields(3).Value;//单位
            tr[index].getElementsByTagName("td")[30].getElementsByTagName("input")[0].value=objrs.Fields(5).Value;//国标
            tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value="采";//毛坯
            tr[index].getElementsByTagName("td")[32].getElementsByTagName("input")[0].value="标";//状态
            tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;//理论重量
            
            var caigoudw=objrs.Fields(3).Value.substring(objrs.Fields(3).Value.indexOf('-')+1,objrs.Fields(3).Value.length);
            if(caigoudw.indexOf("(米")>-1||caigoudw.indexOf("(m")>-1||caigoudw.indexOf("(M")>-1||caigoudw.indexOf("-米)")>-1||caigoudw.indexOf("-m)")>-1||caigoudw.indexOf("-M)")>-1)
            {
                ;
            }
            else
            {
                tr[index].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;
                tr[index].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;
                tr[index].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;
                tr[index].getElementsByTagName("td")[27].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;
            }
          }
          else
          {
            //RM_NAME,RM_MWEIGHT,RM_GUIGE,RM_CAIZHI,RM_UNIT
            tr[index].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=objrs.Fields(0).Value;//名称
            tr[index].getElementsByTagName("td")[25].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//材料规格
            tr[index].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=objrs.Fields(1).Value;//规格
            tr[index].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value=objrs.Fields(2).Value;//材质
            tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value=objrs.Fields(3).Value;//单位
            tr[index].getElementsByTagName("td")[26].getElementsByTagName("input")[0].value=objrs.Fields(4).Value;//理论重量
            tr[index].getElementsByTagName("td")[30].getElementsByTagName("input")[0].value=objrs.Fields(5).Value;//国标

            if(objrs.Fields(0).Value.indexOf("钢板")>-1||objrs.Fields(0).Value.indexOf("钢格板")>-1||objrs.Fields(0).Value.indexOf("花纹板")>-1)
            {
                tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value="板";//毛坯
                if(objrs.Fields(0).Value.indexOf("钢格板")>-1)
                {
                    tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value="采";//毛坯
                    tr[index].getElementsByTagName("td")[32].getElementsByTagName("input")[0].value="成交";//状态
                }
            }
            else if(objrs.Fields(0).Value.indexOf("圆钢")>-1)
            {
                tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value="圆钢";//毛坯
            }
            else if(objrs.Fields(0).Value.indexOf("型钢")>-1||objrs.Fields(0).Value.indexOf("扁钢")>-1||objrs.Fields(0).Value.indexOf("焊管")>-1||objrs.Fields(0).Value.indexOf("焊接管")>-1||objrs.Fields(0).Value.indexOf("无缝管")>-1||objrs.Fields(0).Value.indexOf("无缝钢管")>-1||objrs.Fields(0).Value.indexOf("槽钢")>-1||objrs.Fields(0).Value.indexOf("角钢")>-1||objrs.Fields(0).Value.indexOf("工字钢")>-1||objrs.Fields(0).Value.indexOf("方钢")>-1||objrs.Fields(0).Value.indexOf("矩形管")>-1||objrs.Fields(0).Value.indexOf("轨道")>-1||objrs.Fields(0).Value.indexOf("管")>-1||objrs.Fields(0).Value.indexOf("铜棒")>-1)
            {
                tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value="型";//毛坯
            }
            
            //物料编码不为01.01时，不体现
            if(marid!=""&&(marid.indexOf("01.01.")>0||marid.indexOf("01.08.")>0||marid.indexOf("01.11.")>0))
            {
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("select")[0].value="Y";
            }
            else
            {
                tr[index].getElementsByTagName("td")[8].getElementsByTagName("select")[0].value="N";
            }
          }
          
          if(tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].value=="")
          {
             alert("程序无法识别该物料【毛坯】，请手动输入,否则无法计算重量！！！");
             tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].focus();
             tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].style.background="yellow";
          }
          else//计算重量
          {
              tr[index].getElementsByTagName("td")[31].getElementsByTagName("input")[0].style.background="white";
              autoshuliang(tr[index].getElementsByTagName("td")[12].getElementsByTagName("input")[0]);
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
   if(tr[index].getElementsByTagName("td")[29].getElementsByTagName("input")[0].value.indexOf("(米-")>-1)
   {
      tr[index].getElementsByTagName("td")[0].style.backgroundColor ='#55DF55';
      tr[index].getElementsByTagName("td")[0].title="采购单位为\"米\"，按【材料总长】提计划！！！";
   }
   else
   {
      tr[index].getElementsByTagName("td")[0].style.backgroundColor ='#EFF3FB';
   }
}

/*验证输入总序格式*/
function verify(input)
{
    var zongxu=document.getElementById(input.id);
    table=document.getElementById(getClientId().Id1);
    tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    if(zongxu.value.trim()!="")
    {
        if(!/^([0-9]+|[0-9]+(\.[1-9]{1}[0-9]*)*)$/g.test(zongxu.value))
        {
            alert( "您输入的总序为"+zongxu.value.trim()+"；输入格式有误,请重新输入！！！\r\r提示：\r\r正确的输入格式为：m.n...(m、n为正整数) ");
            ///////////tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].style.background="";
            //document.getElementById(input.id).value="";
            //////zongxu.focus();
        }
    }
    else
    {
       //alert("总序不能为空！！！");
       document.getElementById(input.id).value="";
       return false;
    }
}
//BOM输入，自动添加序号
function autoZongxu(input)
{
  var obj;
  if(document.getElementById("ckbXuhao")!=null)
  {
     obj=document.getElementById("ckbXuhao");
  }
  else if(document.getElementById("ctl00_PrimaryContent_ckbXuhao")!=null)
  {
     obj=document.getElementById("ctl00_PrimaryContent_ckbXuhao");
  }
  
  if(obj.checked)
  {
    table=document.getElementById(getClientId().Id1);
    tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
    if(index==1)
    {
       return;
    }
    var zongxu=table.rows[index-1].cells[6].getElementsByTagName("input")[0].value;
    if(zongxu=="")
    {
       return;
    }
    var tablerows=table.rows.length;
    if(document.getElementById(input.id).value=="")
    {
      if(index<tablerows)
      {
        if(zongxu.indexOf(".")>0)
        {
            var tt=zongxu.split('.');
            var now=parseInt(tt[tt.length-1])+1;
            var temp=zongxu.substring(0,zongxu.lastIndexOf('.'));
            var now_index=temp+"."+now;
            document.getElementById(getClientId().Id1).rows[index].cells[6].getElementsByTagName("input")[0].value=now_index;
            document.getElementById(getClientId().Id1).rows[index].cells[3].getElementsByTagName("input")[0].value=now_index;
        }
        else
        {
            var now_index=parseInt(zongxu)+1;
            document.getElementById(getClientId().Id1).rows[index].cells[6].getElementsByTagName("input")[0].value=now_index;
            document.getElementById(getClientId().Id1).rows[index].cells[3].getElementsByTagName("input")[0].value=now_index;

        }
      }
    }
  }
}

/*插入行提示*/
function insert()
{
    table=document.getElementById(getClientId().Id1);
    obj=table.getElementsByTagName("input");
    objstr = '';
    for(var i=0;i<obj.length;i++)
    {
        if(obj[i].type.toLowerCase()=="checkbox" && obj[i].value!="")
        {
           if( obj[i].checked)
           {    
              objstr="checked";
              break;
           }
        }
    }
    if(objstr=="checked")
    {
        document.getElementById(getClientId().Id3).value="1" ;
    }
    else            
    {
        alert("请指定要插入行的位置!");
        return false;
    }
}

/*原始数据输入确定是否删除*/
function check()
{
    table=document.getElementById(getClientId().Id1);
    tr=table.getElementsByTagName("tr");
    var count=0;
    for(var i=1;i<tr.length;i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("span")[0].getElementsByTagName("input")[0];
        if(obj.checked)
        {    
           count=i;
           break;
        }
    }
    if(count>0)
    {
       var i=confirm('确定删除吗？');
       if(i==true)
       {
            document.getElementById(getClientId().Id2).value=count ;
       }
       else
       {
            document.getElementById(getClientId().Id2).value="0" ;
            return false;
       }
    }
    else            
    {
        alert("请选择要删除的行！");
        return false;
    }
}

//工艺流程改变
function ChangGY(obj)
{
   if(obj.value!="")
   {
       var rowsindex=obj.parentNode.parentNode.rowIndex;
       obj.parentNode.parentNode.parentNode.rows[rowsindex].cells[8].getElementsByTagName("select")[0].value="Y";
   }
}

//工艺流程改变
function ChangKu(obj)
{
   if(obj.value=="库")
   {
       var rowsindex=obj.parentNode.parentNode.rowIndex;
       obj.parentNode.parentNode.parentNode.rows[rowsindex].cells[8].getElementsByTagName("select")[0].value="Y";
   }
}