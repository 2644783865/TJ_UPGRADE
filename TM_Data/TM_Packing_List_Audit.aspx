<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Packing_List_Audit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Packing_List_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    装箱单审批     
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script language="javascript" type="text/javascript">
        //**********弹出技术部人员子窗口***********************
    var i;
    array=new Array();
    function SelTechPersons1()
    {
       i=window.showModalDialog('TM_Tech_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no");
       if(i!==null)
       {
           array=i.split(' ');
           document.getElementById('<%=txt_first.ClientID%>').innerText=array[0];
           document.getElementById('<%=firstid.ClientID%>').value=array[1];
       } 
       else
       {
           document.getElementById('<%=txt_first.ClientID%>').innerText="";
       }
    }
    function SelTechPersons2()
    {
        i=window.showModalDialog('TM_Tech_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
        if(i!=null)
        {
            array=i.split(' ');
            document.getElementById('<%=txt_second.ClientID%>').innerText=array[0];
            document.getElementById('<%=secondid.ClientID%>').value=array[1];
        }
        else
       {
           document.getElementById('<%=txt_second.ClientID%>').innerText="";
       }
    }
    function SelTechPersons3()
    {
        i=window.showModalDialog('TM_Tech_persons.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
        if(i!=null)
        {
            array=i.split(' ');
            document.getElementById('<%=txt_third.ClientID%>').innerText=array[0];
            document.getElementById('<%=thirdid.ClientID%>').value=array[1];
       }
       else
       {
           document.getElementById('<%=txt_third.ClientID%>').innerText="";
       }
    }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0" AutoPostBack="false">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="装箱单表" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper" style="height:472px">
                <div style="height:6px" class="box_top"></div>
                <div class="box-outer">
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td style=" font-size:large; text-align:center;" colspan="8">装箱单</td>
                    </tr>
                    <tr>
                        <td style="width:16%" align="right">General Contractor(承包单位):</td>
                        <td colspan="3" style="width:38%"><asp:Label id="Gen_Contractor" runat="server"/></td>
                        <td style="width:10%" align="right">Owner(业主):</td>
                        <td colspan="3"><asp:Label id="Owner" runat="server"/></td> 
                    </tr>
                    <tr>
                        <td align="right">Vehicle or Ship No.(船号):</td>
                        <td><asp:Label id="Ship_No" runat="server"/></td>
                        <td style="width:16%" align="right">Consignment Date(发货日期):</td>
                        <td><asp:Label id="Con_Date" runat="server"/></td>
                        <td style="width:10%" align="right">Page of(页数):</td>
                        <td><asp:Label id="Page" runat="server"/></td>
                        <td align="right">Signature(签字):</td>
                        <td><asp:Label id="Signature" runat="server"/></td>
                    </tr>
                    <tr>
                        <td align="right">Systerm No.(系统号):</td>
                        <td><asp:Label id="Sys_No" runat="server"/></td>
                        <td align="right">Name of Goods(货物名称):</td>
                        <td  colspan="5"><asp:Label id="Goods_Name" runat="server"/></td>
                    </tr>
                    </table>
                </div>
                
                <div class="box-outer">
                    <asp:Panel ID="Panel1" runat="server"  style="height: 372px; width: 100%; position:static">
                        <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333">
                            <FixRowColumn FixRowType="Header,Pager" TableHeight="372px" TableWidth="100%" FixColumns="0" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            <asp:BoundField DataField="PL_NO" HeaderText="序号" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_PACKAGENO" HeaderText="箱号" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_ITEMNO" HeaderText="设备号" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_PACKNAME" HeaderText="包装名称" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_MARKINGNO" HeaderText="标识号" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_PACKQLTY" HeaderText="包装数量" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_PACKTYPE" HeaderText="包装类型" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_PACKDIML" HeaderText="长(cm)" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_PACKDIMW" HeaderText="宽(cm)" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_PACKDIMH" HeaderText="高(cm)" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_TOTALVOLUME" HeaderText="体积(m3)" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_SINGLENETWGHT" HeaderText="单净重" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_SINGLEGROSSWGHT" HeaderText="单毛重" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_TOTALGROSSWGHT" HeaderText="总毛重" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_DESCRIPTION" HeaderText="原因陈述" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            <asp:BoundField DataField="PL_OUTLINEDRAWING" HeaderText="外形图" >
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </yyc:SmartGridView>
                        </asp:Panel>
                </div>
                    <input id="packlist_no" type="text" runat="server" style="display:none"/>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="审  核" TabIndex="1">
            <ContentTemplate>
             <div class="box-wrapper" style="height:472px">
                <div style="height:6px" class="box_top"></div>
                <div class="box-outer">
                <table width="100%">
                 <tr><td>
                    <asp:Button ID="btnsubmit" runat="server" onclick="btnsubmit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"  />
                </td></tr>
                <tr>
                    <td style=" font-size:large; text-align:center;height:43px">
                        装箱单
                        <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false"/>
                    </td>
                </tr>
                </table>
                </div>
                <div class="box-outer">
                <asp:Panel ID="Panel2" runat="server"  style="height: 376px; width: 100%; position:static">
                    <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                        <tr>
                            <td style="width:10%" align="center">项目名称</td>
                            <td style="width:40%"><asp:Label ID="lab_proname" runat="server" Width="100%"/></td>
                            <td style="width:10%" align="center">工程名称</td>
                            <td style="width:40%"><asp:Label ID="lab_engname" runat="server" Width="100%"/></td>
                            <asp:Label ID="tsa_id" runat="server" Visible="false"></asp:Label>
                        </tr>
                        <tr>
                            <td align="center">编制</td>
                            <td><asp:Label ID="txt_editor" runat="server" Width="100%"></asp:Label></td>
                            <td align="center">编制日期</td>
                            <td><asp:Label ID="txt_plandate" runat="server" Width="100%"/></td>   
                        </tr>
                        <tr>
                            <td align="center">主管审核</td>
                            <td colspan="3">
                            <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                <tr style="height: 25px">
                                <td align="center" style="width:10%">审批人</td>
                                <td style="width:20%">
                                    <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                    <input id="firstid" type="text" runat="server" readonly="readonly" style="display:none" />
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
                                    <input id="secondid" type="text" runat="server" readonly="readonly" style="display:none" />
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
                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display:none" />
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
