<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Out_Source_Change_Audit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_Source_Change_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术外协变更审批          
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script language="javascript" type="text/javascript">
        //**********弹出技术部人员子窗口***********************
    function SelTechPersons1()
    {
       var i=window.showModalDialog('TM_Tech_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       document.getElementById('<%=txt_first.ClientID%>').innerText=i;
       //document.getElementById('<%=txt_first.ClientID%>').value=i;
    }
    function SelTechPersons2()
    {
        var i=window.showModalDialog('TM_Tech_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       document.getElementById('<%=txt_second.ClientID%>').innerText=i;
    }
    function SelTechPersons3()
    {
        var i=window.showModalDialog('TM_Tech_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       document.getElementById('<%=txt_third.ClientID%>').innerText=i;
    }
    </script>


<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top" ActiveTabIndex="0" AutoPostBack="false">
<cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="变更单" TabIndex="0">
<ContentTemplate>
<div class="box-wrapper" style="height:468px">
<div style="height:6px" class="box_top"></div>
    <div class="box-outer">
        <table width="100%">
        <tr>
            <td colspan="8">
                <asp:Button ID="btnsave" runat="server" Text="保 存" onclick="btnsave_Click" Visible="false"/>
            </td>
        </tr>
        <tr>
            <td style=" font-size:large; text-align:center;" colspan="8">技术外协变更</td>
        </tr>
        <tr>
            <td style="width: 8%" align="right">生产制号:</td>
            <td style="width: 15%"><asp:Label ID="tsa_id" runat="server" Width="100%"/></td>
            <td style="width: 8%" align="right">项目编号:</td>
            <td style="width: 25%"><asp:Label ID="out_no" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">项目名称:</td>
            <td style="width: 14%"><asp:Label ID="lab_proname" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">工程名称:</td>
            <td style="width: 14%"><asp:Label ID="lab_engname" runat="server" Width="100%" /></td>
                <input id="status" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
        </tr>
        <tr>
            <td style="width:8%" align="right">批准日期:</td>
            <td colspan="3"><asp:Label ID="txt_approval" runat="server" Width="100%"/></td>
            <td style="width:8%" align="right">编制日期:</td>
            <td colspan="3"><asp:Label ID="txt_plandate" runat="server" Width="100%"/></td>   
        </tr>
        </table>
    </div>
<div class="box-outer" style="height:378px">
<cc1:TabContainer ID="TabContainer2" runat="server" Width="100%" TabStripPlacement="Top" ActiveTabIndex="0" AutoPostBack="false">
<cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="原记录" TabIndex="0">
<ContentTemplate>
    <asp:Panel ID="Panel2" runat="server"  style="height: 320px; width: 100%; position:static">
         <yyc:SmartGridView ID="GridView2" width="100%" CssClass="toptable grid" 
            runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
             <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="320px" TableWidth="100%" FixColumns="0" />
        <%--</asp:GridView>--%>
        </yyc:SmartGridView>
    </asp:Panel>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="变更" TabIndex="1">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat="server"  style="height: 320px; width: 100%;position:static">
         <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" 
                runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
            
            <%--<asp:GridView ID="GridView1" width="100%" CssClass="toptable grid"
                runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                  ShowFooter="true" onrowdatabound="GridView1_RowDataBound">--%>
                 <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                 <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="out_id" runat="server" Text='<%#Eval("OSL_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="部件名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_name" runat="server" Width="70px" Text='<%# Eval("OSL_NAME") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标识" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_biaozhi" runat="server" Width="110px" Text='<%# Eval("OSL_BIAOSHINO") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_guige" runat="server" Width="80px" Text='<%# Eval("OSL_GUIGE") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_caizhi" runat="server" Width="60px" Text='<%# Eval("OSL_CAIZHI") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_unitwght" runat="server" Width="80px" Text='<%# Eval("OSL_UNITWGHT") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_num" runat="server" Width="60px" Text='<%# Eval("OSL_NUMBER") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_totalwght" runat="server" Width="80px" Text='<%# Eval("OSL_TOTALWGHTL") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="外委部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_wdepname" runat="server" Width="80px" Text='<%# Eval("OSL_WDEPNAME") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="加工要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_process" runat="server" Width="80px" Text='<%# Eval("OSL_REQUEST") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="加工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_date" runat="server" Width="80px" onclick="setday(this)" Text='<%# Eval("OSL_REQDATE") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="交货地点" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="lab_place" runat="server" Width="80px" Text='<%# Eval("OSL_DELSITE") %>'></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95"/>
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableHeight="320px" TableWidth="100%" FixColumns="0" />
            <%--</asp:GridView>--%>
            </yyc:SmartGridView>
    </asp:Panel>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="新纪录" TabIndex="2">
<ContentTemplate>
    <asp:Panel ID="Panel4" runat="server"  style="height: 320px; width: 100%; position:static">
         <yyc:SmartGridView ID="GridView3" width="100%" CssClass="toptable grid" 
            runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
           <Columns>
             <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
            <FixRowColumn FixRowType="Header,Pager" TableHeight="320px" TableWidth="100%" FixColumns="0" />
         </yyc:SmartGridView>
     </asp:Panel>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</div>
</div> 
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
<ContentTemplate>
<div class="box-wrapper" style="height:468px">
   <div style="height:6px" class="box_top"></div>
   <div class="box-outer">
    <table width="100%">
         <tr>
            <td>
            <asp:Button ID="btnsubmit" runat="server" Text="" onclientclick="return confirm(&quot;确定提交吗？&quot;)" onclick="btnsubmit_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"  />
            </td>
        </tr>
        <tr>
            <td style=" font-size:large; text-align:center; height:43px">技术外协变更
                <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false"/>
            </td>
        </tr>
    </table>
    </div>
    <div class="box-outer">
    <asp:Panel ID="Panel3" runat="server"  style="height: 372px; width: 100%; position:static">
        <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
            <tr>
                <td style="width:10%" align="center">项目名称</td>
                <td style="width:40%"><asp:Label ID="proname" runat="server" Width="100%"/></td>
                <td style="width:10%" align="center">工程名称</td>
                <td style="width:40%"><asp:Label ID="engname" runat="server" Width="100%"/></td>
            </tr>
            <tr>
                <td align="center">编制</td>
                <td><asp:Label ID="txt_editor" runat="server" Width="100%"></asp:Label></td>
                <td align="center">编制日期</td>
                <td><asp:Label ID="plandate" runat="server" Width="100%"/></td>   
            </tr>
            <tr>
                <td align="center">主管审核</td>
                <td colspan="3">
                <table width="100%" cellpadding="4" cellspacing="1" border="1">
                    <tr style="height: 25px">
                    <td align="center" style="width:10%">审批人</td>
                    <td style="width:20%">
                        <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                        <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()"><asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server"/>
                                选择
                        </asp:HyperLink>
                    </td>
                    <td align="center" style="width:10%">审核结论</td>
                    <td align="center" style="width:20%">
                        <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Height="20px">                
                            <asp:ListItem Text="同意" value="4" ></asp:ListItem>
                            <asp:ListItem Text="不同意" Value="3" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="center" style="width:10%">审核时间</td>
                    <td style="width:20%">
                        <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="6">
                        <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
                    </td>
                    </tr>
                </table> 
                </td>
            </tr>
            <tr>
                <td align="center">部门领导</td>
                <td colspan="3">
                <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                <tr>
                    <td align="center" style="width:10%">审批人</td>
                    <td style="width:20%">
                        <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                        <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()"><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                选择
                    </asp:HyperLink>
                    </td>
                    <td align="center" style="width:10%">审核结论</td>
                    <td align="center" style="width:20%">
                    <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Height="20px">                
                        <asp:ListItem Text="同意" value="6" ></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="5" ></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                    <td align="center" style="width:10%">审核时间</td>
                    <td style="width:20%">
                        <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                    </td>
                </tr>
                    <tr>
                        <td colspan="6">
                            <asp:TextBox ID="second_opinion" runat="server" Height="42px" 
                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td align="center">主管经理</td>
                <td colspan="3">
                <table  align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                <tr>
                <td align="center" style="width:10%">审批人</td>
                <td style="width:20%">
                    <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                    <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()"><asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                    选择
                    </asp:HyperLink>
                </td>
                <td align="center" style="width:10%">审核结论</td>
                <td align="center" style="width:20%">
                    <asp:RadioButtonList ID="rblthird" RepeatColumns="2" runat="server" Height="20px">                
                        <asp:ListItem Text="同意" value="8" ></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="7" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="center" style="width:10%">审核时间</td>
                <td style="width:20%">
                    <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                </td>
                </tr>
                <tr>
                <td colspan="6">
                    <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%" Height="42px"></asp:TextBox>
                </td>
                </tr>
                </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
   </div> 
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer>
</asp:Content>

