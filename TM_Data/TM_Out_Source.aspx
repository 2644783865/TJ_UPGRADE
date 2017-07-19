<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Out_Source.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_Source" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术外协     
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
   <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
   <script src="../JS/PDMN.js" type="text/javascript" charset="GB2312"></script>
   <script src="../JS/TM_ShowInput.js" type="text/javascript" charset="GB2312"></script> 
   <script src="../JS/MS_Adjust.js" type="text/javascript" charset="GB2312"></script>
   <script language="javascript" type="text/javascript">
     function EasyCpyJGYQ(obj)
     {
        var ckbEasy=document.getElementById("ckbEasy");
        if(ckbEasy.checked)
        {
           if(obj.value=="")
           {
             obj.value=document.getElementById("txtContent").value;
           }  
        }
        var ckbClear=document.getElementById("ckbClear");
        if(ckbClear.checked)
        {
           obj.value="";
        }
     }
     function EasyCpyJGSJ(obj)
     {
        var ckbEasy=document.getElementById("ckbEasy");
        if(ckbEasy.checked)
        {
           if(obj.value=="")
           {
            obj.value=document.getElementById("<%=txtDate.ClientID %>").value;
           } 
        }
        var ckbClear=document.getElementById("ckbClear");
        if(ckbClear.checked)
        {
           obj.value="";
        }
     }
     function EasyCpyJHDD(obj)
     {
        var ckbEasy=document.getElementById("ckbEasy");
        if(ckbEasy.checked)
        {
           if(obj.value=="")
           {
             obj.value=document.getElementById("txtPlace").value;
           }  
        }
        var ckbClear=document.getElementById("ckbClear");
        if(ckbClear.checked)
        {
           obj.value="";
        }
     }
     
     function ClearContent()
     {
        var table=document.getElementById("<%=GridView1.ClientID %>");
        if(table!=null)
        {
           var type=window.prompt("请输入要【清空列】对应数值:\r全部-0 加工要求-1 加工日期-2 交货地点-3 ","0");
           if(type!="0"&&type!="1"&&type!="2"&&type!="3")  
           {
              alert("输入了无法识别内容，程序已终止！！！");
              return false;
           }
           
           var _tablerows=table.rows.length;
           var pattem=/^\d+$/;//数量验证
           
           
           var startindex=window.prompt("请输入要【清空列】的【起始行】:","1");
              if(!pattem.test(startindex))
              {
                  alert("输入起始行有误，程序已终止！！！");
                  return false;
              }
              else if(startindex==0)
              {
                  alert("输入起始行不能为0，程序已终止！！！");
                  return false;
              }
              else if(parseInt(startindex)+1>parseInt(_tablerows))
              {
                  alert("输入起始行超出最大行数，程序已终止！！！");
                  return false;
              }    
                     
              var endindex=window.prompt("请输入要【清空列】的【结束行】:",parseInt(_tablerows)-1);  
              if(!pattem.test(endindex))
              {
                  alert("输入结束行有误，程序已终止！！！");
                  return false;
              }
              else if(endindex==0)
              {
                  alert("输入结束行不能为0，程序已终止！！！");
                  return false;
              }
              else if(parseInt(endindex)+1>parseInt(_tablerows))
              {
                  alert("输入结束行超出最大行数，程序已终止！！！");
                  return false;
              }           
              
           var _deleteindex=parseInt(type)+12;
           for(var i=startindex;i<=endindex;i++)
           {
               if(type=="0")
               {
                  table.rows[i].cells[13].getElementsByTagName("input")[0].value="";
                  table.rows[i].cells[14].getElementsByTagName("input")[0].value="";
                  table.rows[i].cells[15].getElementsByTagName("input")[0].value="";
               }
               else
               {
                  table.rows[i].cells[_deleteindex].getElementsByTagName("input")[0].value="";
               }
           }
        }
     }
     
     function AddContent()
     {
        var table=document.getElementById("<%=GridView1.ClientID %>");
        if(table!=null)
        {
           var type=window.prompt("请输入要【添加列】对应数值:\r全部-0 加工要求-1 加工日期-2 交货地点-3 ","0");
           if(type!="0"&&type!="1"&&type!="2"&&type!="3")  
           {
              alert("输入了无法识别内容，程序已终止！！！");
              return false;
           }
           
           var _tablerows=table.rows.length;
           var pattem=/^\d+$/;//数量验证
           
           
           var startindex=window.prompt("请输入要【添加列】的【起始行】:","1");
              if(!pattem.test(startindex))
              {
                  alert("输入起始行有误，程序已终止！！！");
                  return false;
              }
              else if(startindex==0)
              {
                  alert("输入起始行不能为0，程序已终止！！！");
                  return false;
              }
              else if(parseInt(startindex)+1>parseInt(_tablerows))
              {
                  alert("输入起始行超出最大行数，程序已终止！！！");
                  return false;
              }    
                     
              var endindex=window.prompt("请输入要【添加列】的【结束行】:",parseInt(_tablerows)-1);  
              if(!pattem.test(endindex))
              {
                  alert("输入结束行有误，程序已终止！！！");
                  return false;
              }
              else if(endindex==0)
              {
                  alert("输入结束行不能为0，程序已终止！！！");
                  return false;
              }
              else if(parseInt(endindex)+1>parseInt(_tablerows))
              {
                  alert("输入结束行超出最大行数，程序已终止！！！");
                  return false;
              }           
              
           var _addindex=parseInt(type)+12;
           
           var _sjyq=document.getElementById("<%=txtDate.ClientID %>").value;
           var _jgyq=document.getElementById("txtContent").value;
           var _jhdd=document.getElementById("txtPlace").value;
           
           for(var i=startindex;i<=endindex;i++)
           {
               if(type=="0")
               {
                  table.rows[i].cells[13].getElementsByTagName("input")[0].value=_jgyq;
                  table.rows[i].cells[14].getElementsByTagName("input")[0].value=_sjyq;
                  table.rows[i].cells[15].getElementsByTagName("input")[0].value=_jhdd;
               }
               else if(_addindex==14)
               {
                  table.rows[i].cells[_addindex].getElementsByTagName("input")[0].value=_sjyq;
               }
               else if(_addindex==13)
               {
                  table.rows[i].cells[_addindex].getElementsByTagName("input")[0].value=_jgyq;
               }
               else if(_addindex==15)
               {
                  table.rows[i].cells[_addindex].getElementsByTagName("input")[0].value=_jhdd;
               }
           }
        }
     }
   </script>   
   <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
   <div class="box-inner">
   <div class="box_right">
    <div class="box-title">
        <table width="100%">
        <tr>
            <td style="width:25%">生产制号：<asp:Label ID="tsa_id" runat="server" />
              <input id="wx_list" type="text" runat="server" readonly="readonly" value="" style="display: none" />
            </td>
            <td style="width:25%">项目名称：
                <asp:Label ID="lab_proname" runat="server" />
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                <input id="outsource_no" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                <input id="status" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td style="width:25%">工程名称：
                <asp:Label ID="lab_engname" runat="server" />
                <input id="eng_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            </td>
            <td>
            &nbsp;&nbsp; <input id="Button1" type="button" onclick="ClearContent();" value="清 空" />
             &nbsp;&nbsp;
              <input id="Button2" type="button"  onclick="AddContent();" value="添 加" />&nbsp;&nbsp;
            </td>
            <td align="center"><div runat="server" id="div_show_zero"><asp:CheckBox ID="ckbShowZero" runat="server" OnCheckedChanged="ckbShowZero_OnCheckedChanged" AutoPostBack="true" />显示不可提交计划项</div></td>
         </tr>
         </table>
     </div>
 </div>
</div>

 <div class="box-wrapper">
        <div class="box-outer">
        <table width="100%">
           <tr>
            <td style="width:15%" align="center">
              操作：<asp:DropDownList ID="ddloperate" runat="server" onchange="Dochk(this.options[this.selectedIndex].value)">
                    <asp:ListItem Text="-请选择-" Value="-请选择-"></asp:ListItem>
                    <asp:ListItem Text="全选" Value="全选"></asp:ListItem>
                    <asp:ListItem Text="勾选" Value="勾选"></asp:ListItem>
                    <asp:ListItem Text="取消" Value="取消"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_concel" runat="server" Text="取 消" OnClick="btn_concel_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="btnsave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_gosh" runat="server" Text="gosh" OnClick="btn_gosh_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
            </td>
            <td>
            <asp:Button ID="btnCheck" runat="server" Text="审核" Visible="false" OnClientClick="return confirm('确认下推审核吗？');"
                     OnClick="btnCheck_Click" />
            </td>
            <td>
                <input id="ckbClear" type="checkbox" />清空&nbsp;&nbsp;&nbsp;<input id="ckbEasy" type="checkbox" />自动添加&nbsp;&nbsp;&nbsp;&nbsp;【加工要求】<input id="txtContent" type="text" />【加工日期】
                <asp:TextBox ID="txtDate" ReadOnly="true" Width="80" runat="server"></asp:TextBox>
               <cc1:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                        TargetControlID="txtDate">
                    </cc1:CalendarExtender>
                【交货地点】<input id="txtPlace" style="width:80px;" type="text" /></td>
           </tr>
        </table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server" Style="height: 500px; width: 99%; overflow: scroll; position: relative">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    <asp:TemplateField>
                       <ItemTemplate>
                          <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" />
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                       <ItemTemplate>
                          <asp:Label ID="Index" runat="server"  Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OSL_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center"   />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)" DataFormatString="{0:f2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)"  DataFormatString="{0:f2}" ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"   ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center"  />
                    <asp:BoundField DataField="OSL_NOTE" HeaderText="备注"   ItemStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="false" />
                    <asp:TemplateField HeaderText="加工要求" ItemStyle-Width="120px" >
                        <ItemTemplate>
                            <input ID="txt_request" runat="server" style="border-style:none; width:100%" type="text" onfocus="EasyCpyJGYQ(this);" onkeydown="grControlFocus(this)"  ondblclick="ShowInput(this);" 
                            value='<%#Eval("OSL_REQUEST") %>' /><br />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="加工日期" ItemStyle-Width="100px"   >
                        <ItemTemplate>
                            <input ID="txt_time" runat="server" style="border-style:none; width:100%" type="text"  onfocus="EasyCpyJGSJ(this);"  onkeydown="grControlFocus(this)"
                            value='<%#Eval("OSL_REQDATE") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="交货地点" ItemStyle-Width="60px"     >
                        <ItemTemplate>
                            <input ID="txt_place" runat="server" style="border-style:none; width:100%" type="text" onkeydown="grControlFocus(this)"  onfocus="EasyCpyJHDD(this);" 
                            value='<%#Eval("OSL_DELSITE") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false"  ItemStyle-Wrap="false" >
                        <ItemTemplate>
                            <asp:Label ID="lab_ID" runat="server" Text='<%#Eval("OSL_ID") %>'></asp:Label>
                            <asp:Label ID="lblxuhao" runat="server" Text='<%#Eval("OSL_NEWXUHAO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" Wrap="false" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />
                            没有数据！</asp:Panel>
                </asp:Panel>   
                </ContentTemplate>
                </asp:UpdatePanel>          
        </div>
    </div>
 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
<ProgressTemplate>
       <div style="position: absolute; top: 70%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
