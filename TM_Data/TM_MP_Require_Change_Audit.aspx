<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MP_Require_Change_Audit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Require_Change_Audit" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划变更审批         
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
    //**********自动求和***********************
    window.onload = function setClf()
    {
        if(document.getElementById('<%=panelid.ClientID%>').value=="1")
        {
            var table=document.getElementById("<%=GridView1.ClientID%>");
            var tr=table.getElementsByTagName("tr"); 
            var pattem=/^\d+(\.\d+)?$/; 
            var sum=0;   
            for(i=1;i<tr.length-1;i++)
            {
                 var num;
                 //alert(tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value);
                 if(pattem.test(tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value))
                 {
                    num=parseFloat(tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value);
                 }
                 else
                 {
                     num=0;
                     alert('提示:输入数据格式不正确!');
                     return;
                 }
                 sum+=num;
            }
            tr[tr.length-1].getElementsByTagName("td")[5].innerHTML="合计：";
            tr[tr.length-1].getElementsByTagName("td")[6].innerHTML=tr[tr.length-2].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
            tr[tr.length-1].getElementsByTagName("td")[7].innerHTML=sum.toFixed(2);
        }
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
                <asp:Button ID="btnsave" runat="server" Text="保 存" onclick="btnsave_Click" 
                    Visible="False"/>
            </td>
        </tr>
        <tr>
            <td style=" font-size:large; text-align:center;" colspan="8">材料需用计划变更</td>
        </tr>
        <tr>
            <td style="width: 8%" align="right">生产制号:</td>
            <td style="width: 15%"><asp:Label ID="tsa_id" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">项目编号:</td>
            <td style="width: 25%"><asp:Label ID="mp_no" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">项目名称:</td>
            <td style="width: 14%"><asp:Label ID="lab_proname" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">工程名称:</td>
            <td style="width: 14%"><asp:Label ID="lab_engname" runat="server" Width="100%" /></td>
                <caption>
                    <input ID="status" runat="server" readonly="readonly" style="display:none" 
                        type="text"></input>
                    <input ID="pro_id" runat="server" readonly="readonly" style="display:none" 
                        type="text"></input>
            </caption>
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
<cc1:TabContainer  ID="TabContainer2" runat="server" Width="100%" 
        ActiveTabIndex="1">
<cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="原记录">
<ContentTemplate>
    <asp:Panel ID="Panel2" runat="server"  style="height: 320px; width: 100%; position:static">
    <yyc:SmartGridView ID="GridView2" width="100%" CssClass="toptable grid" 
    runat="server" AutoGenerateColumns="False" CellPadding="4" ShowFooter="True"
    ForeColor="#333333" onrowdatabound="GridView2_RowDataBound">
    <FixRowColumn FixRowType="Header,Pager" TableHeight="320px" TableWidth="100%" FixColumns="0" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
     <asp:TemplateField HeaderText="序号">
        <ItemTemplate>
            <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" Width="30px" />
    </asp:TemplateField>
    <asp:BoundField DataField="MP_ZONGXU" HeaderText="总序">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_MARID" HeaderText="物料编码">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_NAME" HeaderText="材料名称">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_GUIGE" HeaderText="规格">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质">
        <ItemStyle HorizontalAlign="Center" Width="60px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_UNIT" HeaderText="单位">
        <ItemStyle HorizontalAlign="Center" Width="60px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_NUMBER" HeaderText="需用数量">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_USAGE" HeaderText="用途">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_TYPE" HeaderText="材料类别">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_TIMERQ" HeaderText="时间要求">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_ENVREFFCT" HeaderText="环保影响">
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
    <asp:BoundField DataField="MP_NOTE" HeaderText="备注">
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </yyc:SmartGridView>
</asp:Panel>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="变更" TabIndex="1">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat="server"  style="height: 320px; width: 100%;position:static">
    <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" 
            runat="server" AutoGenerateColumns="False" CellPadding="4" ShowFooter="True" 
            ForeColor="#333333">
        <FixRowColumn FixRowType="Header,Pager" TableHeight="320px" TableWidth="100%" FixColumns="0" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
         <asp:TemplateField HeaderText="序号">
            <ItemTemplate>
                <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ID" Visible="False">
            <ItemTemplate>
                <asp:Label ID="mp_id" runat="server" Text='<%#Eval("MP_ID") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:BoundField DataField="MP_ZONGXU" HeaderText="总序">
            <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
        <asp:TemplateField HeaderText="物料编码">
                <ItemTemplate>
                    <asp:TextBox ID="lab_ID" runat="server" Width="120px" 
                        Text='<%# Eval("MP_MARID") %>' ontextchanged="lab_ID_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="lab_ID_AutoCompleteExtender" runat="server" 
                        DelimiterCharacters="" Enabled="True" ServicePath="~/Ajax.asmx" TargetControlID="lab_ID"
                         UseContextKey="True" ServiceMethod="HmCode" CompletionInterval="1" 
                         MinimumPrefixLength="1" CompletionSetCount="10">
                    </cc1:AutoCompleteExtender>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="120px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="材料名称">
                <ItemTemplate>
                    <asp:TextBox ID="txt_name" runat="server" Width="80px" Text='<%# Eval("MP_NAME") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="规格">
                <ItemTemplate>
                    <asp:TextBox ID="txt_guige" runat="server" Width="80px" Text='<%# Eval("MP_GUIGE") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="材质">
                <ItemTemplate>
                    <asp:TextBox ID="txt_caizhi" runat="server" Width="48px" Text='<%# Eval("MP_CAIZHI") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="48px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="单位">
                <ItemTemplate>
                    <asp:TextBox ID="txt_unit" runat="server" Width="48px" Text='<%# Eval("MP_UNIT") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="48px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="需用数量">
                <ItemTemplate>
                    <asp:TextBox ID="txt_num" runat="server" Width="60px" Text='<%# Eval("MP_NUMBER") %>' 
                     onblur="setClf()" BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="60px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="用途">
                <ItemTemplate>
                    <asp:TextBox ID="txt_use" runat="server" Width="80px" Text='<%# Eval("MP_USAGE") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="材料类别">
                <ItemTemplate>
                    <asp:TextBox ID="txt_type" runat="server" Width="80px" Text='<%# Eval("MP_TYPE") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="时间要求">
                <ItemTemplate>
                    <asp:TextBox ID="txt_time" runat="server" onclick="setday(this)" Text='<%# Eval("MP_TIMERQ") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:TemplateField>
        <asp:TemplateField HeaderText="环保影响">
                <ItemTemplate>
                    <asp:TextBox ID="txt_influence" runat="server" Width="80px" Text='<%# Eval("MP_ENVREFFCT") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txt_note" runat="server" Text='<%# Eval("MP_NOTE") %>' BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>  
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95"/>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </yyc:SmartGridView>
    </asp:Panel>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="新纪录" TabIndex="2">
<ContentTemplate>
    <asp:Panel ID="Panel4" runat="server"  style="height: 320px; width: 100%; position:static">
     <yyc:SmartGridView ID="GridView3" width="100%" CssClass="toptable grid" 
runat="server" AutoGenerateColumns="False" CellPadding="4" ShowFooter="true"
ForeColor="#333333" onrowdatabound="GridView3_RowDataBound">
<FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
<RowStyle BackColor="#EFF3FB" />
<Columns>
 <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
    <ItemTemplate>
        <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
<asp:BoundField DataField="MP_ZONGXU" HeaderText="总序" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_MARID" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_NAME" HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
<asp:BoundField DataField="MP_UNIT" HeaderText="单位" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
<asp:BoundField DataField="MP_NUMBER" HeaderText="需用数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_USAGE" HeaderText="用途" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_TYPE" HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_TIMERQ" HeaderText="时间要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_ENVREFFCT" HeaderText="环保影响" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
<asp:BoundField DataField="MP_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center"/>
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
<input id="panelid" type="text" runat="server" readonly="readonly" 
        style="display:none"/>
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
            <asp:Button ID="btnsubmit" runat="server" Text="" onclientclick="return confirm(&quot;确定提交吗？&quot;)" onclick="btnsubmit_Click" />
                &nbsp;&nbsp;
            <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"  />
            </td>
        </tr>
        <tr>
            <td style=" font-size:large; text-align:center; height:43px">
                材料需用计划变更
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
