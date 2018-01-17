<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_PCHZ_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_PCHZ_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品采购汇总审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 80%;
            border: solid 1px #E5E5E5;
        }
        .tab tr
        {
            height: 30px;
        }
        .tab tr td
        {
            border: solid 1px #E5E5E5;
            border-collapse: collapse;
        }
    </style>
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="../PC_Data/FixTable.css" rel="stylesheet" />
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />
    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    
    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>
    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>
    <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <script src="../JS/PickPersons.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function xr2() {
            $("#hidPerson").val("person2");
            Sel2();
        }

        function xr3() {
            $("#hidPerson").val("person3");
            Sel3();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txt_HT_SHR1.ClientID %>").val(r.st_name);
                $("#<%=SHR1id.ClientID %>").val(r.st_id);
            }
            if (id == "person2") {
                $("#<%=txt_HT_SHR2.ClientID %>").val(r.st_name);
                $("#<%=SHR2id.ClientID %>").val(r.st_id);
            }
            if (id == "person3") {
                $("#<%=txt_HT_SHR3.ClientID %>").val(r.st_name);              
                $("#<%=SHR3id.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>
       
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        <asp:Button ID="btndelete" runat="server" Text="删除行" onclick="btndelete_Click" OnClientClick="return alert('确定删除此条目吗？')" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btndaochu" runat="server" Text="导出" OnClick="btndaochu_click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Close" Text="返回" OnClick="close" runat="server" />&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0" AutoPostBack="false">
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="申请采购明细" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div style="margin: 0px 0px 0px 10px">
                            <table width="90%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                                <tr>
                                    <td style="font-size: x-large; text-align: center;">
                                        办公用品采购申请单
                                        <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                            Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode"
                                            runat="server"></asp:Label>
                                        <asp:Label ID="LabelState" runat="server" Visible="False"></asp:Label>
                                        <input type="text" id="InputColour" style="display: none" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单&nbsp;&nbsp;&nbsp;人：<asp:Label ID="LabelDoc"
                                            runat="server"></asp:Label>
                                        <asp:Label ID="LabelDocCode" runat="server" Visible="False"></asp:Label>
                                    </td>
                              <td>
                                        &nbsp;&nbsp;&nbsp;总&nbsp;&nbsp;&nbsp;额：<asp:Label ID="lbljine" runat="server"></asp:Label>
                                    </td>
                                   
                                </tr>
                                <tr align="center">
                                
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="txt_note"
                                        runat="server" Width="600px" TextMode="MultiLine"></asp:TextBox>
                                    <asp:Label ID="state"
                                            runat="server" Visible="False"></asp:Label></tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelBody" runat="server" Style="height: 350px;">
                            <div style="width: 100%; margin: 0 auto">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                    CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Width="15px" CssClass="checkBoxCss"  />
                                                <input type="hidden" runat="server" id="hidID" value='<%#Eval("ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                                <input type="hidden" runat="server" id="hidPCCode" value='<%#Eval("PCCODE") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WLBM" DataFormatString="{0:F2}" HeaderText="编码">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLNAME" DataFormatString="{0:F2}" HeaderText="名称">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLMODEL" DataFormatString="{0:F2}" HeaderText="规格型号">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLUNIT" DataFormatString="{0:F2}" HeaderText="单位">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLNUM" DataFormatString="{0:F2}" HeaderText="数量">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLPRICE" DataFormatString="{0:F2}" HeaderText="单价">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLJE" DataFormatString="{0:F2}" HeaderText="金额">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num" DataFormatString="{0:F2}" HeaderText="库存数量">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DEPNAME" DataFormatString="{0:F2}" HeaderText="申请部门">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Note" DataFormatString="{0:F2}" HeaderText="备注">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                        </asp:BoundField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPanel2" HeaderText="审批">
            <ContentTemplate>
                <div style="width: 100%" align="center">
                    <asp:Panel runat="server" ID="panSP" Width="80%">
                        <asp:Panel runat="server" ID="tb">
                            <table width="80%">
                                <tr>
                                    <td align="right" id="tdSPLX">
                                        <asp:RadioButtonList ID="rblShdj" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" 
                                        AutoPostBack="true" OnSelectedIndexChanged="rblShdj_SelectedIndexChanged">
                                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审核" Value="2"  Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size:  x-large; text-align: center; height: 43px">
                                        办公用品汇总审批
                                        <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" 
                                            Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="FirstSHPanel">
                            <table id="Table1" align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid" runat="server"
                                border="1">
                                <tr style="height: 25px">
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR1" runat="server" onfocus="this.blur()" Width="120px"></asp:TextBox>
                                        <asp:TextBox ID="SHR1id" runat="server" style="display:none;"></asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR1_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR1_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR1_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="SecondSHPanel">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR2" runat="server" onfocus="this.blur()" Width="120px"></asp:TextBox>
                                        <asp:TextBox ID="SHR2id" runat="server" style="display:none;"></asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR2" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR2_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR2_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR2_JY" runat="server" Height="42px" TextMode="MultiLine"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="ThirdSHPanel" Visible="false">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR3" runat="server" onfocus="this.blur()" Width="120px"></asp:TextBox>
                                        <asp:TextBox ID="SHR3id" runat="server" style="display:none;"></asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR3" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr3()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR3_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR3_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR3_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                </div>
                
                
                
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
                                    <input id="dep" name="dept" value="05">
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
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                        保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                            onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                                data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
                    <input id="hidPerson" type="hidden" value="" />
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
