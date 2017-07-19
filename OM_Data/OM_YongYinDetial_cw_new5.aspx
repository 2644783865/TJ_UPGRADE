<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_YongYinDetial_cw_new5.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_YongYinDetial_cw_new5" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用印申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
    $(document).ready(function(){
               $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").hide();
                    $("#<%=Panel4.ClientID%>").hide();
                    $("#<%=Panel6.ClientID%>").hide();
                }
                else if ($(this).val() == "2") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").hide();
                    $("#<%=Panel6.ClientID%>").hide();
                }
                else if ($(this).val() == "3") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").show();
                    $("#<%=Panel6.ClientID%>").hide();
                }
                else if ($(this).val() == "4") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").show();
                    $("#<%=Panel6.ClientID%>").show();
                }
            });

    });
        //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
          function SelTechPersons3() {
            $("#hidPerson").val("third");
            SelPersons();
        }
          function SelTechPersons4() {
            $("#hidPerson").val("fourth");
            SelPersons();
        }

        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
              if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            if (id == "fourth") {
                $("#<%=txt_fourth.ClientID %>").val(r.st_name);
                $("#<%=fourthid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
       
       
        function rblSPJB_onchange() {
            $("#tdSPLX input:radio:checked").each(function() {
                if ($(this).val() == "1") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").hide();
                    $("#<%=Panel4.ClientID%>").hide();
                    $("#<%=Panel6.ClientID%>").hide();
                }
                else if ($(this).val() == "2") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").hide();
                    $("#<%=Panel6.ClientID%>").hide();
                }
                else if ($(this).val() == "3") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").show();
                    $("#<%=Panel6.ClientID%>").hide();
                }
                else if ($(this).val() == "4") {
                    $("#<%=Panel2.ClientID%>").show();
                    $("#<%=Panel3.ClientID%>").show();
                    $("#<%=Panel4.ClientID%>").show();
                    $("#<%=Panel6.ClientID%>").show();
                }
            });
        }

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="float: right">
        <asp:Button ID="btnAudit" runat="server" Text="提交审批" OnClick="btnAudit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnsubmit" runat="server" Text="保 存" OnClientClick="return Test();"
            OnClick="btnsubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnBack" onclick="window.location.href='OM_YongYinList.aspx';"
            value="返 回" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="hidden" id="hidConext" runat="server" />
        <input type="hidden" id="hidState" runat="server" />
        <input type="hidden" id="hidAction" runat="server" />
        <input type="hidden" id="hidType" runat="server" />
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="基本信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper1">
                    <div class="box-outer" style="text-align: center;">
                        <div style="text-align: center;">
                            <h2>
                                <asp:Label runat="server" ID="lblType"></asp:Label></h2>
                        </div>
                        <br />
                        <asp:Panel ID="Panel0" runat="server">
                            <table width="1000px">
                                <tr>
                                    <td>
                                        申请单号
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        制单人姓名：
                                    </td>
                                    <td>
                                        <asp:Label ID="lb1" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        所在部门：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDep" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        制单时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTime" Enabled="false" runat="server" class="easyui-datebox" Width="120px" data-options="editable:false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        增加行:<asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"
                                            CssClass="center"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" />
                                        &nbsp; &nbsp; &nbsp;<asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
                                    </td>
                                    <td>
                                        备注：
                                    </td>
                                    <td colspan="6">
                                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="600px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="Panel1" runat="server">
                            <div style="text-align: center; overflow: auto; height: 300px">
                                <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <asp:Repeater ID="Det_Repeater" runat="server" >
                                        <HeaderTemplate>
                                            <tr class="tableTitle headcolor">
                                                <td align="center">
                                                    <asp:Label ID="Label" Text="序 号" runat="server" Font-Bold="true" Width="50px"></asp:Label>
                                                </td>
                                                <td align="center" id="th1" runat="server">
                                                    <strong>项目名称</strong>
                                                </td>
                                                <td align="center">
                                                    <strong>文件名称(用途)</strong>
                                                </td>
                                                <td align="center">
                                                    <strong>类型</strong>
                                                </td>
                                                <td align="center">
                                                    <strong>数量</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                <td>
                                                    <div style="width: 50px">
                                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                        <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                        </asp:CheckBox></div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTaskId" TextMode="MultiLine" runat="server" Width="150px" Text='<%#Eval("TaskId")  %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt1" runat="server" Width="300px" Text='<%#Eval("Name")  %>' Height="30px"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlType" runat="server" SelectedValue='<%#Eval("Type") %>'>
                                                        <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="总经理姓名章" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt3" runat="server" name="txt3" Width="200px" Text='<%#Eval("Num") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                    没有记录!</asp:Panel>
                            </div>
                        </asp:Panel>
                        <div style="float: left; padding-top: 10px">
                        </div>
                    </div>
                </div>
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td style="width: 150px">
                            附件：
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Label ID="lblpathName" runat="server" Visible="false"></asp:Label>
                            <asp:Button ID="bntupload" runat="server" Text="上 传" OnClick="bntupload_Click" CausesValidation="False" />
                            <asp:Label ID="filesError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lbreport" runat="server" Visible="False"></asp:Label>
                            <asp:GridView ID="gvfileslist" runat="server" AutoGenerateColumns="False" PageSize="5"
                                CssClass="toptable grid" CellPadding="4" DataKeyNames="ID" ForeColor="#333333"
                                Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("ID") %>' ID="lbid"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FILENAME" HeaderText="文件名称" ItemStyle-Width="60%">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UPLOADDATE" HeaderText="上传时间">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                OnClick="imgbtndelete_Click" CausesValidation="False" ToolTip="删除" Width="15px"
                                                Height="15px" />
                                        </ItemTemplate>
                                        <ControlStyle Font-Size="Small" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="下载">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtndownload" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Width="15px"
                                                Height="15px" />
                                        </ItemTemplate>
                                        <ControlStyle Font-Size="Small" />
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small"
                                    Height="10px" />
                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="审核信息" TabIndex="2">
            <ContentTemplate>
                            <div>
                    <p style="color: Red">
                        <asp:Label ID="Label1" runat="server" Text="审批人请依次选择本部门负责人,主管副总经理，综合办公室负责人，总经理"></asp:Label></p>
                </div>
                <div class="box-outer">
                    <asp:Panel runat="server" ID="Panel5">
                        <table width="100%">
                            <tr style="height: 25px">
                                <td style="text-align: right">
                                    审批类型：
                                </td>
                                <td id="tdSPLX" colspan="4">
                                    <asp:RadioButtonList runat="server" ID="rblSPJB" RepeatDirection="Horizontal" RepeatColumns="4"
                                        Width="320px" onchange="rblSPJB_onchange()" Style="margin: auto">
                                        <asp:ListItem Text="一级审批" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="二级审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="三级审批" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="四级审批" Value="4" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel2">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center" style="width: 70px;">
                                    一级审核</br></br><p style="color: Red">
                                        <asp:Label ID="lbtips1" runat="server" Text=""></asp:Label></p>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                    <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult1" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="first_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel3">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center" style="width: 70px;">
                                    二级审核</br></br><p style="color: Red">
                                        <asp:Label ID="lbtips2" runat="server" Text=""></asp:Label>
                                    </p>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult2" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="second_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel4">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center" style="width: 70px;">
                                    三级审核</br></br><p style="color: Red">
                                        <asp:Label ID="lbtips3" runat="server" Text=""></asp:Label></p>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult3" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="third_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel6">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center" style="width: 70px;">
                                    四级审核</br></br><p style="color: Red">
                                        <asp:Label ID="lbtips4" runat="server" Text=""></asp:Label></p>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审核人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_fourth" runat="server" Width="80px"></asp:TextBox>
                                                <input id="fourthid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect4" runat="server" CssClass="hand" onClick="SelTechPersons4()">
                                                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结果
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList runat="server" ID="rblResult4" RepeatColumns="2">
                                                    <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="fourth_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="fourth_opinion" runat="server" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <div>
        <div>
            <div id="win" visible="false">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                按部门查询：
                            </td>
                            <td>
                                <input id="dep" name="dept" value="03">
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 430px; height: 230px">
                    <table id="dg">
                    </table>
                </div>
            </div>
            <div id="buttons" style="text-align: right" visible="false">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>
