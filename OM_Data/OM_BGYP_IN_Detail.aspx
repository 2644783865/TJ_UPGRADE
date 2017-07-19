<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_IN_Detail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_IN_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    新增入库
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var state;
        function allsel() {
            var table = document.getElementById("ctl00_PrimaryContent_GridView1");
            var tr = table.getElementsByTagName("tr");
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0] != null) {
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
                }
            }
        }

        function cancelsel() {
            var table = document.getElementById("ctl00_PrimaryContent_GridView1");
            var tr = table.getElementsByTagName("tr");
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0] != null) {
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                    //tr[i].style.backgroundColor='#EFF3FB'; 
                }
            }
        }

        function consel() {
            table = document.getElementById("ctl00_PrimaryContent_GridView1");
            tr = table.getElementsByTagName("tr"); //这里的tr是一个数组
            for (var i = 1; i < (tr.length - 1); i++) {
                obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if (obj.type.toLowerCase() == "checkbox" && obj.value != "") {
                    if (obj.checked) {
                        obj.checked = true;
                        //                obj.parentNode.parentNode.style.backgroundColor ='#55DF55'; 
                        for (var j = i + 1; j < tr.length; j++) {
                            var nextobj = tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                            if (nextobj != null) {

                                if (nextobj.type.toLowerCase() == "checkbox" && nextobj.value != "") {
                                    if (nextobj.checked) {
                                        for (var k = i + 1; k < j + 1; k++) {
                                            tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
                                            //                                    tr[k].style.backgroundColor ='#55DF55'; 
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


    </script>

    <script type="text/javascript">

        function autoCode(input) {
            var arrString = $(input).val();
            var $tr = $(input).parent().parent();
            if (arrString != null) {
                var array = arrString.split('|');
                //圆珠笔|蓝色|0.5|1-1-4-2|41
                $tr.find("input[name*=name]").val(array[0]);
                $tr.find("input[name*=canshu]").val(array[1]);
                $tr.find("input[name*=uprice_dj]").val(array[2]);
                $tr.find("input[name*=unit]").val(array[3]);
                $tr.find("input[name*=maId]").val(array[4]);
                $tr.find("input[name*=sId]").val(array[5]);
                $tr.find("input[name*=num]").val("1");
                $tr.find("input[name*=price_je]").val(array[2]);

            }
        }
        function sum(input) {
            var $tr = $(input).parent().parent();
            var dj = $tr.find("input[name*=uprice_dj]").val();
            var num = $tr.find("input[name*=num]").val();
            $tr.find("input[name*=price_je]").val((dj * num).toFixed(2));
        }
    
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="margin: 0px 0px 0px 10px">
                <table width="99%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                    <tr>
                        <td style="width: 50%" align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btninsert" runat="server" Text="插入行" OnClick="btninsert_Click" CausesValidation="False" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btndelete" runat="server" Text="删除行" OnClick="btndelete_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" OnClientClick="return checkPage();" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="Close" type="button" value="关闭" onclick="closewin()" runat="server" />&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
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
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                        </td>
                        <td style="font-size: x-large; text-align: center;" colspan="2">
                            办公用品入库单
                            <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;：<asp:Label ID="LabelDoc" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
            </div>
            <asp:Panel ID="PanelBody" runat="server" Style="height: 350px;">
                <div style="width: 80%; margin: 0 auto">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                        CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="15px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Width="15px" CssClass="checkBoxCss" />
                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="采购订单编号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="编码">
                                <ItemTemplate>
                                    <asp:TextBox ID="maId" runat="server" Text='<%#Eval("maId") %>' onchange="autoCode(this)"
                                        name="maId" Enabled="false"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                        ServicePath="~/OM_Data/Ajax.asmx" TargetControlID="maId" UseContextKey="True"
                                        CompletionInterval="10">
                                    </cc1:AutoCompleteExtender>
                                    <input id="CODE" runat="server" name="code" type="hidden" value='<%#Eval("CODE") %>' />
                                    <input id="sId" runat="server" name="sId" type="hidden" value='<%#Eval("sId") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名称">
                                <ItemTemplate>
                                    <input id="name" name="name" runat="server" style="border-style: none; width: 80px"
                                        type="text" readonly="readonly" value='<%#Eval("name") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="规格型号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="canshu" name="canshu" runat="server" style="border-style: none; width: 80px"
                                        type="text" value='<%#Eval("canshu") %>' readonly="readonly" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="unit" name="unit" runat="server" style="border-style: none; width: 40px"
                                        type="text" value='<%#Eval("Unit") %>' onkeyup="grControlFocus(this)" readonly="readonly" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="数量" ItemStyle-BackColor="#FFFFCC" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="num" name="num" runat="server" style="border-style: none; width: 80px"
                                        type="text" value='<%#Eval("num") %>' onkeyup="sum(this)" />
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFFCC" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="uprice_dj" name="uprice_dj" runat="server" style="border-style: none;
                                        width: 80px" type="text" value='<%#Eval("uprice") %>' onkeyup="sum(this)"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="price_je" name="price_je" runat="server" style="border-style: none; width: 80px"
                                        type="text" value='<%#Eval("price") %>' onfocus="this.blur()" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="note" runat="server" style="border-style: none; width: 200px" type="text"
                                        value='<%#Eval("note") %>' onkeyup="grControlFocus(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
